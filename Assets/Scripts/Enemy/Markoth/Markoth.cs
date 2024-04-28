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
		
        States.Add(EnemyState.Idle, new State(EnemyState.Idle));
        States[EnemyState.Idle].Actions.Add(new IdleAction(rb));

        States.Add(EnemyState.Attacking, new State(EnemyState.Attacking));
        States[EnemyState.Attacking].Actions.Add(new AttackActionMarkoth(rb, GameObject.FindWithTag("Player"), prefab));
		
        States.Add(EnemyState.Dead, new State(EnemyState.Dead));
        States[EnemyState.Dead].Actions.Add(new DeadAction());

        ChangeState(EnemyState.Idle);
        StartCoroutine(IdleRoutine());
    }


    private void FixedUpdate()
    {
        States[currentState].ExecuteStateActions(this);
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
