using UnityEngine;

public class Sword : MonoBehaviour
{
    public float attackSpeed = 10f; 
    private Rigidbody2D rb;
    private Transform target;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Attack()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * attackSpeed;
            Debug.Log("Sword attacking towards " + target.name);
        }
    }
}