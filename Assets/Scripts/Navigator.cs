using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    Transform targetItem;
    GameObject wisp;

    // Update is called once per frame
    public void Start()
    {
        wisp = transform.GetChild(0).gameObject;
    }
    void Update()
    {
        SetClosestTarget();
        if (targetItem)
        {
            if (!wisp.activeInHierarchy) { wisp.SetActive(true); }
            LookAtTarget(targetItem);
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
        GameObject[] sceneItems = GameObject.FindGameObjectsWithTag("Mandrake");
        if (sceneItems.Length == 0) {
            targetItem = GameObject.FindGameObjectWithTag("Finish").transform;
            return; 
        }
        Transform closestItem = sceneItems[0].transform;
        foreach (GameObject testItem in sceneItems)
        {
            closestItem = GetClosest(closestItem, testItem.transform);
        }

        targetItem = closestItem.GetChild(0);
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
