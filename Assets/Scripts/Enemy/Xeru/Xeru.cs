using System.Collections;
using UnityEngine;

public class Xeru : Enemy {

	private bool _isIdleCorroutineRunning = false;

	// protected override void OnStateChange(){
	// 	base.OnStateChange();
	// 	if (currentState == EnemyState.Attacking){
	// 		StartCoroutine(SpawnRoutine());
	// 	}
	// }

	void Start(){
		rb = GetComponent<Rigidbody2D>();

		States.Add(EnemyState.Idle, new State(EnemyState.Idle));
		States[EnemyState.Idle].Actions.Add(new IdleActionXeru(rb));

		States.Add(EnemyState.Attacking, new State(EnemyState.Attacking));
		States[EnemyState.Attacking].Actions.Add(new AttackActionXeru());
		
		States.Add(EnemyState.Dead, new State(EnemyState.Dead));
		States[EnemyState.Dead].Actions.Add(new DeadAction());

		ChangeState(EnemyState.Idle);
		StartCoroutine(WaitAttack());
	}

	void Update(){
		States[currentState].ExecuteStateActions(this);
		if(!_isIdleCorroutineRunning && currentState == EnemyState.Idle){
			StartCoroutine(WaitAttack());
		}
	}

	IEnumerator WaitAttack(){
		_isIdleCorroutineRunning = true;
		while (currentState == EnemyState.Idle){
			yield return new WaitForSeconds(3f);
			ChangeState(EnemyState.Attacking);
			_isIdleCorroutineRunning = false;
		}
	}
}