using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class CCTV : Enemy
{
    //public float detectionRange = 10f;
    //public Transform player;
    private Enemy[] enemies;
    private PA_Warrior warrior;

    protected override void Start()
    {
        base.Start();
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        //player = GameObject.FindGameObjectWithTag("Player").transform;

        enemies = new Enemy[enemyObjects.Length];

        for (int i = 0; i < enemyObjects.Length; i++)
        {
            enemies[i] = enemyObjects[i].GetComponent<Enemy>();
        }
        //Debug.Log(enemyObjects.Length);
    }

    
    void Update()
    {

        //if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        //{
        //    Enemy closestMonster = FindClosestMonster();
        //    if (closestMonster != null)
        //    {
        //        closestMonster.MoveTo(player.position); 
        //    }
        //}

        if (CanSeePlayer())
        {
            // DetectPlayer();
            Enemy closestMonster = FindClosestMonster();
            if (closestMonster != null)
            {
                closestMonster.MoveTo(player_last_position); 
            }
        }
    }

    private Enemy FindClosestMonster()
    {
        Enemy closestMonster = null;
        float closestDistance = float.MaxValue;

        foreach (Enemy monster in enemies)
        {
            if (monster == null) continue; 

            float distance = Vector3.Distance(transform.position, monster.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestMonster = monster;
            }
        }

        return closestMonster;
    }

    // 탐지 범위를 원뿔 형태로 표시합니다.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 adjustedOrigin = transform.position + transform.forward * 0.3f;

        DrawCone(adjustedOrigin, transform.forward, viewAngle, detectionRange);
    }

    private void DrawCone(Vector3 origin, Vector3 forward, float angle, float range, int segments = 30)
    {
        forward.Normalize();

        Vector3 initialDirection = Quaternion.Euler(0, -angle / 2, 0) * forward;

        // Loop to draw the cone's edges
        for (int i = 0; i < segments; i++)
        {
            Vector3 currentDirection = Quaternion.Euler(0, (angle / segments) * i, 0) * initialDirection;
            Vector3 nextDirection = Quaternion.Euler(0, (angle / segments) * (i + 1), 0) * initialDirection;

            Vector3 currentPoint = origin + currentDirection * range;
            Vector3 nextPoint = origin + nextDirection * range;

            Gizmos.DrawLine(origin, currentPoint);
            Gizmos.DrawLine(currentPoint, nextPoint);
        }
    }

    public void DetectPlayer()
    {
        Enemy closestMonster = FindClosestMonster();
        if (closestMonster != null)
        {
            Debug.Log(closestMonster);
            closestMonster.SetDetectedCCTV(this); 
        }
    }
}
