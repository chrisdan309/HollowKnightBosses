using System.Collections;
using UnityEngine;

public class Xeru : Enemy {

	private bool isIdleCorroutineRunning = false;

	// protected override void OnStateChange(){
	// 	base.OnStateChange();
	// 	if (currentState == EnemyState.Attacking){
	// 		StartCoroutine(SpawnRoutine());
	// 	}
	// }

	void Start(){
		rb = GetComponent<Rigidbody2D>();

		states.Add(EnemyState.Idle, new State(EnemyState.Idle));
		states[EnemyState.Idle].actions.Add(new IdleActionXeru(rb));

		states.Add(EnemyState.Attacking, new State(EnemyState.Attacking));
		states[EnemyState.Attacking].actions.Add(new AttackActionXeru());
		
		states.Add(EnemyState.Dead, new State(EnemyState.Dead));
		states[EnemyState.Dead].actions.Add(new DeadAction());

		ChangeState(EnemyState.Idle);
		StartCoroutine(WaitAttack());
	}

	void Update(){
		states[currentState].ExecuteStateActions(this);
		if(!isIdleCorroutineRunning && currentState == EnemyState.Idle){
			StartCoroutine(WaitAttack());
		}
	}

	IEnumerator WaitAttack(){
		isIdleCorroutineRunning = true;
		while (currentState == EnemyState.Idle){
			yield return new WaitForSeconds(3f);
			ChangeState(EnemyState.Attacking);
			isIdleCorroutineRunning = false;
		}
	}
}