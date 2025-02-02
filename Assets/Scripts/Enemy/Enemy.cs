using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject player;
    public NavMeshAgent Agent { get => agent; }
    public GameObject Player { get => player; }

    public Path path;
    [Header("Sight values")]
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    public float eyeHeight;
    [Header("Weapon values")]
    public Transform gunBarrel;
    [Range(0.1f, 10f)]
    public float fireRate;
    [SerializeField]
    private string currentState;


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
        currentState = stateMachine.activeState.ToString();
    }

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
          

            if (distance < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
               

                if (angleToPlayer <= fieldOfView * 0.5f) // Fixed FOV check
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * (eyeHeight + 0.5f)), targetDirection);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, sightDistance))
                    {
                        Debug.DrawRay(ray.origin, ray.direction * sightDistance, Color.green); // Visible ray
                        

                        if (hit.transform.gameObject == player)
                        {
                            
                            return true;
                        }
                    }
                    else
                    {
                        Debug.DrawRay(ray.origin, ray.direction * sightDistance, Color.red);
                     
                    }
                }
            }
        }
        return false;
    }

}
