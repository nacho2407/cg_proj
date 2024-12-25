using UnityEngine;
using UnityEngine.AI;

public class PA_Warrior : Enemy
{
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    private float nextAttackTime;

    private NavMeshAgent agent;
    private Animator animator;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Debug.Log(CanSeePlayer());
        if (CanSeePlayer())
        {
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
            Idle();
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
        agent.SetDestination(transform.position); // Stop moving while attacking
        animator.SetBool("HasTargetInrange", true);
        // Implement the actual attack logic here, e.g., reduce player health
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

    protected override void Die()
    {
        base.Die();
        animator.SetTrigger("Die");
        agent.isStopped = true; // Stop movement
        // Add any additional cleanup or effects for death
    }
}