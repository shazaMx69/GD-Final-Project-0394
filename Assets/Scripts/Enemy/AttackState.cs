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

            // Smoothly rotate towards the player
            Quaternion targetRotation = Quaternion.LookRotation(enemy.Player.transform.position - enemy.transform.position);
            enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, targetRotation, Time.deltaTime * 5f);

            if (shootTimer > enemy.fireRate)
            {
                Shoot();
            }

            if (moveTimer > Random.Range(3, 7))
            {
                Vector3 randomDirection = Random.insideUnitSphere * 5;
                randomDirection.y = 0; // Keep movement on the same Y level
                enemy.Agent.SetDestination(enemy.transform.position + randomDirection);
                moveTimer = 0;
            }
            enemy.LastKnownPos = enemy.Player.transform.position;
        }
        else
        {
            losePlayerTimer += Time.deltaTime;

            if (losePlayerTimer > 8)
            {
                stateMachine.ChangeState(new SearchState());
            }
        }
    }

    public void Shoot()
    {
        // Check if gunBarrel is assigned
        if (enemy.gunBarrel == null)
        {
            Debug.LogError("Gun barrel is not assigned!");
            return;
        }

        // Load bullet prefab
        GameObject bulletPrefab = Resources.Load("bullet") as GameObject;
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab not found! Check the Resources folder.");
            return;
        }

        // Instantiate bullet
        GameObject bullet = GameObject.Instantiate(bulletPrefab, enemy.gunBarrel.position, enemy.transform.rotation);

        // Calculate shooting direction
        Vector3 shootDirection = (enemy.Player.transform.position - enemy.gunBarrel.position).normalized;

        // Apply force to the bullet
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb == null)
        {
            Debug.LogError("Bullet prefab does not have a Rigidbody!");
            return;
        }

        bulletRb.velocity = shootDirection * 40;
        Debug.Log("Shoot");

        shootTimer = 0;
    }
}
