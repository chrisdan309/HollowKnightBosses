using UnityEngine;

public abstract class Action
{
    public abstract void Execute(Enemy enemy, float deltaTime);
}

public class AttackAction : Action
{
    private int damage;

    public AttackAction(int damage)
    {
        this.damage = damage;
    }

    public override void Execute(Enemy enemy, float deltaTime)
    {
        // Modificar para realizar algún otro tipo de lógica de ataque
        Debug.Log("Attack action executed, damage potential: " + damage);
    }
}


public class SpecialAttackAction : Action
{
    private int specialDamage;

    public SpecialAttackAction(int specialDamage)
    {
        this.specialDamage = specialDamage;
    }

    public override void Execute(Enemy enemy, float deltaTime)
    {
        Debug.Log("Special damage");
    }
}