using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int life = 3;
    public float invulnerabilityTime = 2f;
    [SerializeField] 
    private bool isInvulnerable = false;
    private Rigidbody2D rb;
    private int defaultLayer;
    
    private SpriteRenderer spriteRenderer;
    public Color invulnerableColor = Color.blue;
    private Color originalColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        rb = GetComponent<Rigidbody2D>();
        defaultLayer = gameObject.layer;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ASDASDs");
        if (!isInvulnerable)
        {
            Debug.Log("Entro");
            life--;
            MakeInvulnerable();
            StartCoroutine(PushPlayer(collision));
            // PushPlayer(collision);
            if (life <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void MakeInvulnerable()
    {
        isInvulnerable = true;
        spriteRenderer.color = invulnerableColor;

        Invoke("RemoveInvulnerability", invulnerabilityTime);
    }

    private void RemoveInvulnerability()
    {
        isInvulnerable = false;
        spriteRenderer.color = originalColor;
        gameObject.layer = defaultLayer;  // Vuelve a la capa original
    }

    IEnumerator PushPlayer(Collider2D collision)
    {
        float direction = (transform.position.x - collision.transform.position.x) > 0 ? -1 : 1;
        Debug.Log(direction > 0 ? "Izquierda" : "Derecha");
        
        rb.AddForce(new Vector2(direction * 0.001f, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.8f);
        rb.velocity = new Vector2(0,0);

    }



}
