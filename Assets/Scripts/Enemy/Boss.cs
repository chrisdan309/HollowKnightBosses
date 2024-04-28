using UnityEngine;

public class Boss : Enemy
{
    void Awake(){
        States.Add(EnemyState.Attacking, new State(EnemyState.Attacking));
        States[EnemyState.Attacking].Actions.Add(new AttackAction(1));
        States[EnemyState.Attacking].Actions.Add(new SpecialAttackAction(2));
    }
}