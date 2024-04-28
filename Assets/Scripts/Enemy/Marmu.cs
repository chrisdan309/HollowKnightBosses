using System.Collections;
using UnityEngine;

public class Marmu : Enemy
{
	public float bounceAceleration;
	public float bounceDuration = 8f;
	public float pauseDuration = 2f;
	public float knockbackStrength = 16f;

	private Rigidbody2D _rb;
	private Animator _animator;
	
	void Start(){
		_rb = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
		
		states.Add(EnemyState.Idle, new State(EnemyState.Idle));
		states[EnemyState.Idle].actions.Add(new IdleAction(_rb));

		states.Add(EnemyState.Attacking, new State(EnemyState.Attacking));
		states[EnemyState.Attacking].actions.Add(new BounceAction(bounceAceleration, _rb, GameObject.FindWithTag("Player")));
		
		states.Add(EnemyState.Dead, new State(EnemyState.Dead));
		states[EnemyState.Dead].actions.Add(new DeadAction());

		ChangeState(EnemyState.Idle);
		StartCoroutine(IdleRoutine());
	}

	void FixedUpdate(){
		states[currentState].ExecuteStateActions(this, Time.deltaTime);
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

	IEnumerator AttackRoutine(){

		//start attack animation
		while (currentState == EnemyState.Attacking){
			_animator.SetBool("Bounce", true);
			yield return new WaitForSeconds(bounceDuration);
			ChangeState(EnemyState.Idle);
			StartCoroutine(IdleRoutine());
		}
	}

	IEnumerator IdleRoutine(){

		//start idle animation
		while (currentState == EnemyState.Idle){
			_animator.SetBool("Bounce", false);
			yield return new WaitForSeconds(pauseDuration);
			ChangeState(EnemyState.Attacking);
			StartCoroutine(AttackRoutine());
		}
	}
}
