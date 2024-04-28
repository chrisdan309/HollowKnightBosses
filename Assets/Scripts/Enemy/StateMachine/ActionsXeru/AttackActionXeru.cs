using UnityEngine;

public class AttackActionXeru : Action {
    public override void Execute(Enemy enemy, float deltaTime){

		enemy.transform.GetChild(Random.Range(0,2))
			.GetComponent<Sword>().SetPlayerTarget().Attack();

		enemy.ChangeState(EnemyState.Idle);
    }
}