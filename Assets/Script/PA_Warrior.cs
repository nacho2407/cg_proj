using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PA_Warrior : Enemy
{
    public float attackRange = 10f;
    public float attackCooldown = 1.5f;
    private float nextAttackTime;

    public EnemyGunRayCast gun;
    public Transform gunPivot;

    private NavMeshAgent agent;
    private Animator animator;

    // 추가 체력
    public float pa_warriorHealth = 20f;

    

    
    protected override void Start()
    {
        base.Start();
        health += pa_warriorHealth;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        gun.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        gun.gameObject.SetActive(false);
    }

    void Update()
    {
        //Debug.Log(CanSeePlayer());
        if (CanSeePlayer())
        {
            isCCTVon = false;
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer > attackRange)
            {
                ChasePlayer();
            }
            else
            {
                if (Time.time >= nextAttackTime)
                {
                    AttackPlayer();
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
        }
        else
        {
            if (detectedCCTV != null)
                //if (isCCTVon)
            {
                MoveTo(detectedCCTV.transform.position);
            }
            else
            {
                Idle();
                
            }
        }

        UpdateAnimations();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetBool("HasTargetInrange", false);
    }

    private void AttackPlayer()
    {
        gun.Fire();
        agent.SetDestination(transform.position); 
        animator.SetBool("HasTargetInrange", true);
    }

    private void Idle()
    {
        agent.SetDestination(transform.position);
        animator.SetFloat("position_x", 0);
        animator.SetFloat("position_y", 0);
    }

    private void UpdateAnimations()
    {
        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            Vector3 localVelocity = transform.InverseTransformDirection(agent.velocity);
            animator.SetFloat("position_x", localVelocity.x);
            animator.SetFloat("position_y", localVelocity.z);
        }
        else
        {
            animator.SetFloat("position_x", 0);
            animator.SetFloat("position_y", 0);
        }
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
    }

    protected override void Die()
    {
        //base.Die();
        animator.SetTrigger("Die");
        agent.isStopped = true;

        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
        gun.gameObject.SetActive(false);
        StartCoroutine(HandleDeath());

    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public override void MoveTo(Vector3 targetPosition)
    {
        //base.MoveTo(targetPosition); 
        //Debug.Log("Overrided MoveTo");
        //Debug.Log(targetPosition);
        agent.SetDestination(targetPosition);
        animator.SetBool("HasTargetInrange", false);
        isCCTVon = true;
    }
    private void MoveToDetectedCCTV()
    {
        if (detectedCCTV != null)
        {
            MoveTo(detectedCCTV.transform.position);
        }
    }



}