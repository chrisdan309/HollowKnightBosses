using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public Transform attackPoint; // El punto desde donde se originará el ataque
    public float attackRange = 0.5f; // El rango del ataque
    public int attackDamage = 20; // El daño que hará el ataque
    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    void Attack()
    {
        // Reproducir la animación de ataque
        //animator.SetTrigger("Attack");

        // Detectar enemigos en el rango del ataque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Aplicar daño a esos enemigos
        foreach(Collider2D enemy in hitEnemies)
        {
            // Aquí podrías llamar a un método del enemigo para recibir daño
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }
    
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
