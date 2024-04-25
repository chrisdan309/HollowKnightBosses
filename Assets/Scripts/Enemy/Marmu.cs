using System.Collections;
using UnityEngine;

public class Marmu : Enemy
{
    public float bounceForce;
    public float bounceDuration = 8f;
    public float pauseDuration = 2f;
    public float rotation;
    private Rigidbody2D rb;
    private bool isBouncing = false;

    protected override void OnStateChange()
    {
        base.OnStateChange();
        if (currentState == EnemyState.Attacking)
        {
            StartCoroutine(BounceRoutine());
        }
    }
    
    void Start()
    {
        rotation = 1000f;
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = rotation;
        
        states.Add(EnemyState.Idle, new State(EnemyState.Idle));
        states[EnemyState.Idle].actions.Add(new IdleAction(rb));

        states.Add(EnemyState.Attacking, new State(EnemyState.Attacking));
        states[EnemyState.Attacking].actions.Add(new BounceAction(bounceForce, rb));
        
        states.Add(EnemyState.Dead, new State(EnemyState.Dead));
        states[EnemyState.Dead].actions.Add(new DeadAction());

        ChangeState(EnemyState.Attacking);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Trigger con el jugador!");
            // Lógica trigger plauer
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Colisión con: " + collision.gameObject.name);
    }

    IEnumerator BounceRoutine()
    {
        while (currentState == EnemyState.Attacking)
        {
            isBouncing = true;
            states[currentState].ExecuteStateActions(this);
            yield return new WaitForSeconds(bounceDuration);

            isBouncing = false;
            ChangeState(EnemyState.Idle);
            yield return new WaitForSeconds(pauseDuration);

            ChangeState(EnemyState.Attacking);
        }
    }
}
