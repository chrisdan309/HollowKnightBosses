using System.Collections;
using UnityEngine;

public class Xeru : Enemy {
	public GameObject swordPrefab;

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
		states[EnemyState.Attacking].actions.Add(new SpawnObjectAction(swordPrefab, transform));
		
		states.Add(EnemyState.Dead, new State(EnemyState.Dead));
		states[EnemyState.Dead].actions.Add(new DeadAction());

		ChangeState(EnemyState.Idle);
	}

	void Update(){
		states[currentState].ExecuteStateActions(this, Time.deltaTime);
	}

	IEnumerator SpawnRoutine(){
		while (currentState == EnemyState.Attacking){
			//states[currentState].ExecuteStateActions(this);
			yield return new WaitForSeconds(2f);
		}
	}
}