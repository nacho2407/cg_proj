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
            DetectPlayer();
            Enemy closestMonster = FindClosestMonster();
            if (closestMonster != null)
            {
              
                closestMonster.MoveTo(transform.position); 
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

    // Debug
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
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
