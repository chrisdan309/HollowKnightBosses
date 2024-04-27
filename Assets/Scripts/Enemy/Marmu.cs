using System.Collections;
using UnityEngine;

public class Marmu : Enemy
{
    public float bounceForce;
    public float bounceDuration = 8f;
    public float pauseDuration = 2f;
    public float rotation;
    public float knockbackStrength = 16f;
    private Rigidbody2D _rb;
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
        _rb = GetComponent<Rigidbody2D>();
        _rb.angularVelocity = rotation;
        
        states.Add(EnemyState.Idle, new State(EnemyState.Idle));
        states[EnemyState.Idle].actions.Add(new IdleAction(_rb));

        states.Add(EnemyState.Attacking, new State(EnemyState.Attacking));
        states[EnemyState.Attacking].actions.Add(new BounceAction(bounceForce, _rb));
        
        states.Add(EnemyState.Dead, new State(EnemyState.Dead));
        states[EnemyState.Dead].actions.Add(new DeadAction());

        ChangeState(EnemyState.Attacking);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            //Debug.Log("Trigger con el jugador!");
            // Lógica trigger plauer
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        //Debug.Log("Colisión con: " + collision.gameObject.name);
    }

    public override void TakeDamage(Vector3 pos, float damage)
    {
        base.TakeDamage(pos, damage);
        if (health > 0)
        {
            // Aplica una fuerza de retroceso
            
            // get vector from pos to transform.position
            Vector2 forceDirection = (transform.position - pos).normalized;
            _rb.AddForce(forceDirection * knockbackStrength, ForceMode2D.Impulse);
            Debug.Log("Marmu recibe un retroceso!");

            // Cambia la dirección de Marmu
            //transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
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
