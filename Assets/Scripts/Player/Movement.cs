using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5.0f; // Velocidad de movimiento
    public float jumpForce = 7.0f; // Fuerza del salto
    private Rigidbody2D rb; // Referencia al Rigidbody2D
    public bool isGrounded; // Para verificar si el personaje está en el suelo
    public Transform attackPoint; // El punto desde donde se originará el ataque
    public float attackRange = 0.5f; // El rango del ataque
    public int attackDamage = 20; // El daño que hará el ataque
    public LayerMask enemyLayers;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtenemos el componente Rigidbody2D
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Jump();
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }

        
    }

    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        if (x != 0)
        {
            float xscale = transform.localScale.x;
            float yscale = transform.localScale.y;
            transform.localScale = new Vector3(Mathf.Sign(xscale) * xscale, yscale, 1);
        }
        
        
        Vector2 movement = new Vector2(x * speed, rb.velocity.y);
        rb.velocity = movement;
    }
    
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    
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

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}
