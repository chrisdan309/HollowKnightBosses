using UnityEngine;

public class DeadState : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void EnterDeadState()
    {
        // Detener el movimiento
        if (_rb != null)
        {
            _rb.velocity = Vector2.zero;
        }

        if (_animator != null)
        {
            _animator.SetTrigger("Die");
        }

        GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject, 2f); 
    }
}