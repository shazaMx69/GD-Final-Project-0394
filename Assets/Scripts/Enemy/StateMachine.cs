using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    public PatrolState patrolState;

    public void Initialize()
    {
        //setup the default state
        patrolState = new PatrolState();
        ChangeState(patrolState);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeState != null) 
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        if (newState == null) return; // Prevent null state assignments

        if (activeState != null)
        {
            activeState.Exit();
        }

        activeState = newState;
        activeState.stateMachine = this;
        activeState.enemy = GetComponent<Enemy>(); // Ensure enemy is always assigned

        if (activeState.enemy == null)
        {
            Debug.LogError("Enemy is NULL in ChangeState! Make sure an Enemy component is attached.");
            return;
        }

        activeState.Enter();
    }



}
