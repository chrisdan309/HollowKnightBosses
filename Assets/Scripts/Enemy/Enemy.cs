using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Attacking,
    Dead
}

public class Enemy : MonoBehaviour
{
    public float health;
    public float attackPower;
    public EnemyState currentState;
    public readonly Dictionary<EnemyState, State> states = new();

    void Start()
    {
        ChangeState(EnemyState.Idle);
    }

    protected void ChangeState(EnemyState newState)
    {
        if (currentState == newState) return;
        currentState = newState;
        OnStateChange();
    }

    protected virtual void OnStateChange()
    {
        if (states.ContainsKey(currentState))
        {
            states[currentState].ExecuteStateActions(this);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            ChangeState(EnemyState.Dead);
        }
    }
}