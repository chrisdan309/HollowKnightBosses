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

	private Animator animator;

	// Start is called before the first frame update
	void Start(){
		animator = GetComponent<Animator>();
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
		animator.SetTrigger("Attack");

		// Detectar enemigos en el rango del ataque
		Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(attackPoint.position, new Vector2(attackRange, attackHeight), enemyLayers);

		// Aplicar daño a esos enemigos
		foreach(Collider2D enemy in hitEnemies){
			// Aquí podrías llamar a un método del enemigo para recibir daño
			enemy.GetComponent<Enemy>().TakeDamage(this.transform.position, attackDamage);
		}
	}
	
	void OnDrawGizmosSelected(){
		if (attackPoint == null)
			return;
		
		Gizmos.DrawWireCube(attackPoint.position, new Vector2(attackRange, attackHeight));
	}
}
