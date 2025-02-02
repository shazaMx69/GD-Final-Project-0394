using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    private float shootTimer;
    public override void Enter()
    {
        Debug.Log("Entering Attack State");
        shootTimer = 0; // Reset timers when entering
        moveTimer = 0;
        losePlayerTimer = 0;
    }

    public override void Exit()
    {
        Debug.Log("Exiting Attack State");
    }

    public override void Perform()
    {

        if (enemy.CanSeePlayer())
        {

            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shootTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);

            if (shootTimer > enemy.fireRate)
            {
                Shoot();
            }

            if (moveTimer > Random.Range(3, 7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
        }
        else
        {
            losePlayerTimer += Time.deltaTime;

            if (losePlayerTimer > 8)
            {
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }


    public void Shoot()
    {
        Debug.Log("Shoot");
        shootTimer = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
