using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    public Path path;

    public NavMeshAgent Agent { get => agent; }
    [SerializeField]
    private string currentState;

    private GameObject player;
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    public float eyeHeight;

    // Start is called before the first frame update
    void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        path = FindObjectOfType<Path>(); // ✅ Finds Path anywhere in the scene

        player = GameObject.FindGameObjectWithTag("Player");

        stateMachine.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
    }

    public bool CanSeePlayer()
    {
        if(player != null) 
        {
            // is player close enough to be seen
            if (Vector3.Distance(transform.position, player.transform.position)<sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if(angleToPlayer >= fieldOfView && angleToPlayer <= fieldOfView) 
                {
                    Ray ray = new Ray(transform.position + (Vector3.up*eyeHeight), targetDirection);
                    RaycastHit hit = new RaycastHit();
                    if(Physics.Raycast(ray, out hit,sightDistance)) 
                    {
                        if(hit.transform.gameObject == player)
                        {
                            return true;
                        }
                    }
                    Debug.DrawRay(ray.origin,ray.direction*sightDistance);
                }
            }
        }
        return false;
    }
}
