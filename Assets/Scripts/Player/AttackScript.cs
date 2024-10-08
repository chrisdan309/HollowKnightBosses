using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
	[SerializeField] private Transform attackPoint; // El punto desde donde se originará el ataque
	[SerializeField] private float attackRange = 0.5f; // El rango del ataque
	[SerializeField] private float attackHeight = 0.5f; // La altura del ataque
	[SerializeField] private int attackDamage = 20; // El daño que hará el ataque
	public LayerMask enemyLayers;

	private Animator _animator;
	private static readonly int Attack1 = Animator.StringToHash("Attack");

	// Start is called before the first frame update
	void Start(){
		_animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update(){
		if (Input.GetKeyDown(KeyCode.F)){
			Attack();
		}
	}
	
	// ReSharper disable Unity.PerformanceAnalysis
	void Attack(){
		// Reproducir la animación de ataque
		_animator.SetTrigger(Attack1);

		// Detectar enemigos en el rango del ataque
		Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(attackPoint.position, new Vector2(attackRange, attackHeight), enemyLayers);

		// Aplicar daño a esos enemigos
		foreach(Collider2D enemy in hitEnemies){
			// Aquí podrías llamar a un método del enemigo para recibir daño
			Debug.Log("We hit " + enemy.name + " with the attack.");
			enemy.GetComponent<Enemy>().TakeDamage(this.transform.position, attackDamage);
		}
	}
	
	void OnDrawGizmosSelected(){
		if (attackPoint == null)
			return;
		
		Gizmos.DrawWireCube(attackPoint.position, new Vector2(attackRange, attackHeight));
	}
}
