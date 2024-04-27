using System.Collections;
using UnityEngine;

public class Xeru : Enemy
{
    public GameObject swordPrefab;
    private Rigidbody2D _rb;

    protected override void OnStateChange()
    {
        base.OnStateChange();
        if (currentState == EnemyState.Attacking)
        {
            StartCoroutine(SpawnRoutine());
        }
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        states.Add(EnemyState.Idle, new State(EnemyState.Idle));
        states[EnemyState.Idle].actions.Add(new IdleActionXeru(_rb));

        states.Add(EnemyState.Attacking, new State(EnemyState.Attacking));
        states[EnemyState.Attacking].actions.Add(new SpawnObjectAction(swordPrefab, transform));
        
        states.Add(EnemyState.Dead, new State(EnemyState.Dead));
        states[EnemyState.Dead].actions.Add(new DeadAction());

        ChangeState(EnemyState.Idle);
    }

    IEnumerator SpawnRoutine()
    {
        while (currentState == EnemyState.Attacking)
        {
            states[currentState].ExecuteStateActions(this);
            yield return new WaitForSeconds(2f);
        }
    }
}