using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    private int waypointIndex = 0; // Start at the first waypoint
    public float waitTimer;

    public override void Enter()
    {
        if (enemy == null || enemy.path == null || enemy.path.waypoints == null || enemy.path.waypoints.Count == 0)
            return;

        waypointIndex = 0;
        enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
    }

    public override void Perform()
    {
        PatrolCycle();
    }

    public override void Exit()
    {
        // Cleanup logic if needed
    }

    private void PatrolCycle()
    {
        if (enemy == null || enemy.Agent == null || enemy.path == null) return;

        if (enemy.Agent.remainingDistance < 0.2f && !enemy.Agent.pathPending)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 3)
            {
                waypointIndex = (waypointIndex + 1) % enemy.path.waypoints.Count;
                enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
                waitTimer = 0;
            }
        }

    }
}
