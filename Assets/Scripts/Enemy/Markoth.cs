using System;
using UnityEngine;

public class Markoth : Enemy
{
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
		
        states.Add(EnemyState.Idle, new State(EnemyState.Idle));
        states[EnemyState.Idle].actions.Add(new IdleAction(_rb));

        states.Add(EnemyState.Attacking, new State(EnemyState.Attacking));
        states[EnemyState.Attacking].actions.Add(new AttackActionMarkoth(_rb, GameObject.FindWithTag("Player")));
		
        states.Add(EnemyState.Dead, new State(EnemyState.Dead));
        states[EnemyState.Dead].actions.Add(new DeadAction());

        ChangeState(EnemyState.Attacking);
    }


    private void FixedUpdate()
    {
        states[currentState].ExecuteStateActions(this, Time.deltaTime);
    }
    
}
