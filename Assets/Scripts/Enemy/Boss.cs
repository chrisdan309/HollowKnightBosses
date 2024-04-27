using UnityEngine;

public class Boss : Enemy
{
    void Awake(){
        states.Add(EnemyState.Attacking, new State(EnemyState.Attacking));
        states[EnemyState.Attacking].actions.Add(new AttackAction(1));
        states[EnemyState.Attacking].actions.Add(new SpecialAttackAction(2));
    }
}