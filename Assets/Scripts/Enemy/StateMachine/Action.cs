using UnityEngine;

public abstract class Action {
	public abstract void Execute(Enemy enemy);
}

public class AttackAction : Action {
	private int damage;

	public AttackAction(int damage) {
		this.damage = damage;
	}

	public override void Execute(Enemy enemy)
	{
		// Modificar para realizar algún otro tipo de lógica de ataque
		Debug.Log("Attack action executed, damage potential: " + damage);
	}
}

public class SpecialAttackAction : Action
{
	private int _specialDamage;

	public SpecialAttackAction(int specialDamage)
	{
		_specialDamage = specialDamage;
	}

	public override void Execute(Enemy enemy)
	{
		Debug.Log("Special damage");
	}
}