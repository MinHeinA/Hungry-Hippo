using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorWisp : MonoBehaviour
{
    Transform enemyTarget;
    GameObject wisp;

    public float detectRadius = 4f;

    // Update is called once per frame
    public void Start()
    {
        wisp = transform.GetChild(0).gameObject;
    }
    public void SetTarget(Transform target)
    {
        enemyTarget = target;
    }
    void Update()
    {
        SetClosestTarget();
        float distToEnemy = Vector3.Distance(transform.position, enemyTarget.position);
        Debug.Log(distToEnemy);
        if (enemyTarget && distToEnemy < detectRadius)
        {
            if (!wisp.activeInHierarchy) { wisp.SetActive(true); }
            LookAtTarget(enemyTarget);
        }
        else
        {
            wisp.SetActive(false);
        }
    }

    private void LookAtTarget(Transform target)
    {
        Vector3 diff = target.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90f);
    }
    private void SetClosestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 1)
        {
            Transform closestEnemy = enemies[0].transform;
            foreach (GameObject enemy in enemies)
            {
                closestEnemy = GetClosest(closestEnemy, enemy.transform);
            }

            enemyTarget = closestEnemy.GetChild(0);
        }
        else
        {
            enemyTarget = enemies[0].transform;
        }
        
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        float distToA = Vector3.Distance(transform.position, transformA.position);
        float distToB = Vector3.Distance(transform.position, transformB.position);
        if (distToA > distToB)
        {
            return transformB;
        }
        return transformA;
    }
}
