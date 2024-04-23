using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public int life = 3;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            life--;

            float direction = (transform.position.x - collision.transform.position.x);
            
            if(direction > 0)
            {
                Debug.Log("Izquierda");
                direction = -1;
            }
            else
            {
                Debug.Log("Derecha");
                direction = 1;
            }
            rb.AddForce(new Vector2(direction * 100, 10), ForceMode2D.Impulse);
            if (life <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
