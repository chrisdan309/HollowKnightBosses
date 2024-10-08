using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
	Idle,
	Attacking,
	Dead
}

public class Enemy : MonoBehaviour
{
	public float maxHealth;
	public float health;
	public float attackPower;
	public EnemyState currentState;
	public Rigidbody2D rb;
	public float knockbackStrength = 16f;
	[SerializeField] public Dictionary<EnemyState, State> States = new();
	
	void Start(){
		ChangeState(EnemyState.Idle);
		health = maxHealth;
	}

	public void ChangeState(EnemyState newState){
		if (currentState == newState) return;
		currentState = newState;
		States[currentState].OnStateEntry(this);
	}

	public void TakeDamage(Vector3 pos, float damage)
	{
		health -= damage;
		Vector2 forceDirection = (transform.position - pos).normalized;
		rb.AddForce(forceDirection * knockbackStrength, ForceMode2D.Impulse);
		if (health <= 0)
		{
			ChangeState(EnemyState.Dead);
		}
	}
}