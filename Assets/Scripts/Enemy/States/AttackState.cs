using System.Collections;
using UnityEngine;

public class AttackState : State
{
    private Enemy enemy;
    public float bounceDuration = 8f;
    public AttackState(Enemy enemy) : base(enemy) {}

    public override void Enter()
    {
        Debug.Log("Enemy in Bouncing State.");
        //enemy.SetAnimation("Bounce");
        enemy.StartCoroutine(BounceDuration());
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }


    public override void Exit()
    {
        Debug.Log("Exiting Attack State.");
    }
    
    private IEnumerator BounceDuration()
    {
        yield return new WaitForSeconds(bounceDuration);
        enemy.ChangeState(new IdleState(enemy));
    }
}