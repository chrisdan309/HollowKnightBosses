using UnityEngine;

public class DeadState : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void EnterDeadState()
    {
        // Detener el movimiento
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }

        // Configurar el animator para reproducir la animación de muerte
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // Opcional: desactivar colisiones y otras interacciones
        GetComponent<Collider2D>().enabled = false;

        // Destruir el objeto después de un breve retraso para permitir que la animación se complete
        Destroy(gameObject, 2f);  // Ajusta este tiempo según la duración de la animación de muerte
    }
}