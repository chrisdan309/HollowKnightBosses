using System;
using System.Collections;
using UnityEngine;

public class Markoth : Enemy
{
    public GameObject shield;
    public GameObject prefab;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		
        states.Add(EnemyState.Idle, new State(EnemyState.Idle));
        states[EnemyState.Idle].actions.Add(new IdleAction(rb));

        states.Add(EnemyState.Attacking, new State(EnemyState.Attacking));
        states[EnemyState.Attacking].actions.Add(new AttackActionMarkoth(rb, GameObject.FindWithTag("Player"), prefab));
		
        states.Add(EnemyState.Dead, new State(EnemyState.Dead));
        states[EnemyState.Dead].actions.Add(new DeadAction());

        ChangeState(EnemyState.Idle);
        StartCoroutine(IdleRoutine());
    }


    private void FixedUpdate()
    {
        states[currentState].ExecuteStateActions(this);
    }
    
    IEnumerator AttackRoutine()
    {
        while (currentState == EnemyState.Attacking)
        {
            yield return new WaitForSeconds(6f);
            ChangeState(EnemyState.Idle);
            StartCoroutine(IdleRoutine());
        }
    }
    
    IEnumerator IdleRoutine()
    {
        while (currentState == EnemyState.Idle)
        {
            
            yield return new WaitForSeconds(5f);
            ChangeState(EnemyState.Attacking);
            StartCoroutine(AttackRoutine());
        }
    }
}
