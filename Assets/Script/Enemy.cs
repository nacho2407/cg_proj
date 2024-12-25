using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health = 10f;
    public float viewAngle = 67.5f;
    public float detectionRange = 15f;
    protected Transform player;

    private NavMeshAgent agentEnemy;
    protected bool isCCTVon = false;
    protected CCTV detectedCCTV;

    protected static Vector3 player_last_position = new Vector3(0,0,0); // 마지막으로 확인한 플레이어의 위치 - CCTV 등에서 탐지 시 CCTV 위치로 이동하지 않고 마지막으로 확인한 플레이어의 위치로 이동시키기 위함.

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        agentEnemy = GetComponent<NavMeshAgent>();
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
        // 원뿔 형태의 탐지가 더 좋은 것 같아서 수정하였습니다.
        Vector3 directionToPlayer = player.position - (transform.position + transform.forward * 0.3f);

        if (directionToPlayer.magnitude > detectionRange)
            return false;

        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        // 시야각 체크(원뿔 형태 탐지)
        if (angle < viewAngle / 2f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.forward * 0.3f, directionToPlayer.normalized, out hit, detectionRange))
            {
                // 감시 시점과 감시 대상 사이 장애물이 없으면
                if (hit.collider.CompareTag("Player"))
                {
                    // Debug.Log("Player Detected!");
                    player_last_position = player.position; // 마지막으로 확인한 위치 저장
                    return true;
                // } else {
                    // Debug.Log($"Hit Object: {hit.collider.name}");
                }
            }
        }

        return false;

        // Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);

        // foreach (var hitCollider in hitColliders)
        // {
        //     // 태그가 "Player"인 객체만 감지
        //     if (hitCollider.CompareTag("Player"))
        //     {
        //         Vector3 directionToPlayer = hitCollider.transform.position - transform.position;
        //         float angle = Vector3.Angle(transform.forward, directionToPlayer);

        //         // 플레이어가 시야 내에 있는지 확인
        //         if (angle < viewAngle / 2f)
        //         {
        //             return true;  // 플레이어를 볼 수 있는 경우
        //         }
        //     }
        // }
        // return false;
    }

    public virtual void MoveTo(Vector3 targetPosition)
    {
        if (agentEnemy != null)
        {
            //Debug.Log(targetPosition);
            //Debug.Log(agentEnemy.transform);
            agentEnemy.SetDestination(targetPosition);
        }
    }

 

    public void SetDetectedCCTV(CCTV cctv)
    {
        detectedCCTV = cctv;
    }


}
