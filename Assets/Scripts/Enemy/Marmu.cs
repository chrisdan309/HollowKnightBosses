using System.Collections;
using UnityEngine;

public class Marmu : Enemy
{
    public float bounceForce;
    public float bounceDuration = 8f;
    public float pauseDuration = 2f;
    public float rotation;
    private Rigidbody2D rb;
    private bool isBouncing = false;

    // Start is called before the first frame update
    void Start()
    {
        rotation = 1000f;
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = rotation;
        StartCoroutine(BounceRoutine());
    }

    IEnumerator BounceRoutine()
    {
        while (true)
        {
            isBouncing = true;
            ApplyRandomBounceForce();
            
            yield return new WaitForSeconds(bounceDuration);


            isBouncing = false;
            rb.velocity = Vector2.zero;
            
            yield return new WaitForSeconds(pauseDuration);
        }
    }

    private void ApplyRandomBounceForce()
    {
        
        Vector2 forceDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.AddForce(forceDirection * bounceForce, ForceMode2D.Impulse);
    }


}