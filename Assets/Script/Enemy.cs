using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float sightRange = 100f;
    public float viewAngle = 360f;
    public float detectionRange = 15f;
    protected Transform player;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected bool CanSeePlayer()
    {
        //Vector3 directionToPlayer = player.position - transform.position;
        //float angle = Vector3.Angle(transform.forward, directionToPlayer);
        //Debug.Log($"Direction to Player: {directionToPlayer}, Player Position: {player.position}, Enemy Position: {transform.position}");
        //Debug.Log($"Angle to Player: {angle}, View Angle: {viewAngle / 2f}");


        //if (angle < viewAngle / 2f)
        //{
        //RaycastHit hit;

        //Debug.DrawRay(transform.position, directionToPlayer.normalized * detectionRange, Color.red, 0.1f);
        //Debug.DrawRay(transform.position, directionToPlayer.normalized * detectionRange, Color.green, 0.1f);


        //if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange))
        //if (Physics.SphereCast(transform.position, 1f, directionToPlayer.normalized, out hit, detectionRange))
        //{
        //    Debug.Log($"Hit object: {hit.collider.name}");

        //    if (hit.collider.CompareTag("Player"))
        //    {
        //        return true;
        //    }
        //}


        //}
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);

        foreach (var hitCollider in hitColliders)
        {
            // 태그가 "Player"인 객체만 감지
            if (hitCollider.CompareTag("Player"))
            {
                Vector3 directionToPlayer = hitCollider.transform.position - transform.position;
                float angle = Vector3.Angle(transform.forward, directionToPlayer);

                // 플레이어가 시야 내에 있는지 확인
                if (angle < viewAngle / 2f)
                {
                    return true;  // 플레이어를 볼 수 있는 경우
                }
            }
        }
        return false;
    }


}
