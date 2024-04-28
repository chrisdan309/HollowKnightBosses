using System.Collections;
using UnityEngine;

public class Marmu : Enemy
{
	public float bounceAceleration;
	public float bounceDuration = 8f;
	public float pauseDuration = 2f;

	private Animator _animator;
	private static readonly int Bounce = Animator.StringToHash("Bounce");

	void Start(){
		_animator = GetComponent<Animator>();
		
		States.Add(EnemyState.Idle, new State(EnemyState.Idle));
		States[EnemyState.Idle].Actions.Add(new IdleAction(rb));

		States.Add(EnemyState.Attacking, new State(EnemyState.Attacking));
		States[EnemyState.Attacking].Actions.Add(new BounceAction(bounceAceleration, rb, GameObject.FindWithTag("Player")));
		
		States.Add(EnemyState.Dead, new State(EnemyState.Dead));
		States[EnemyState.Dead].Actions.Add(new DeadAction());

		ChangeState(EnemyState.Idle);
		StartCoroutine(IdleRoutine());
	}

	void FixedUpdate(){
		States[currentState].ExecuteStateActions(this);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player")) {
			//Debug.Log("Trigger con el jugador!");
			// Lógica trigger plauer
		}
	}
	
	IEnumerator AttackRoutine(){

		//start attack animation
		while (currentState == EnemyState.Attacking){
			_animator.SetBool(Bounce, true);
			yield return new WaitForSeconds(bounceDuration);
			ChangeState(EnemyState.Idle);
			StartCoroutine(IdleRoutine());
		}
	}

	IEnumerator IdleRoutine(){

		//start idle animation
		while (currentState == EnemyState.Idle){
			_animator.SetBool(Bounce, false);
			yield return new WaitForSeconds(pauseDuration);
			ChangeState(EnemyState.Attacking);
			StartCoroutine(AttackRoutine());
		}
	}
}
