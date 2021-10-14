using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    Transform target;
    NavMeshAgent agent;
    private NavMeshPath path;
    public float step = 0.1f;
    bool isMoving = false;
    [SerializeField]
    [Range(0f, 4f)]
    float lerpTime = 1f;
    float t = 0f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        path = new NavMeshPath();

    }

    public void FixedUpdate()
    {
        //Debug.Log(isMoving);
        Vector3 targetPos = new Vector3(0f,0f,0f);
        Vector3 direction = new Vector3(0f, 0f, 0f);
        if (!isMoving)
        { 
            agent.CalculatePath(target.position, path);
            direction = path.corners[0] - transform.position;
            Debug.Log(path.corners[0]);
            path.ClearCorners();

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                direction.y = 0f;
            }
            else
            {
                direction.x = 0f;
            }
            isMoving = true;
            
        }
        
        transform.position = Vector3.Lerp(transform.position, direction, lerpTime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        //Debug.Log(t);

        if (t > 0.9f)
        {
            t = 0f;
            isMoving = false;
        }
    }


}