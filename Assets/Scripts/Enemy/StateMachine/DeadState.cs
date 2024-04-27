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

        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject, 2f); 
    }
}