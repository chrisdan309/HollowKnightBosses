using UnityEngine;

public abstract class Action
{
    public abstract void Execute(Enemy enemy);
}

public class AttackAction : Action
{
    private int damage;

    public AttackAction(int damage)
    {
        this.damage = damage;
    }

    public override void Execute(Enemy enemy)
    {
        enemy.TakeDamage(damage);
    }
}

public class SpecialAttackAction : Action
{
    private int specialDamage;

    public SpecialAttackAction(int specialDamage)
    {
        this.specialDamage = specialDamage;
    }

    public override void Execute(Enemy enemy)
    {
        enemy.TakeDamage(specialDamage);
        Debug.Log("Special damage");
    }
}