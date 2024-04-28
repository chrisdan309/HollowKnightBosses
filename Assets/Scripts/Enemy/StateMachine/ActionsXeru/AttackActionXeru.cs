using UnityEngine;

public class AttackActionXeru : Action {
    public override void Execute(Enemy enemy){

		enemy.transform.GetChild(Random.Range(0,2))
			.GetComponent<XeruSword>().SetPlayerTarget().Attack();

		enemy.ChangeState(EnemyState.Idle);
    }
}