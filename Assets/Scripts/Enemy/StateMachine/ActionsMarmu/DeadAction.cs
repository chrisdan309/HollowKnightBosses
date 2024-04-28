using UnityEngine;

public class DeadAction : Action
{
    public override void Execute(Enemy enemy)
    {
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.zero;
        
        // Animator animator = enemy.GetComponent<Animator>();
        // if (animator != null)
        //     animator.SetTrigger("Die");

        Collider2D collider = enemy.GetComponent<Collider2D>();
        if (collider != null)
            collider.enabled = false;
        
        GameObject.Destroy(enemy.gameObject); 
    }
}