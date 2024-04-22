using System.Collections;
using UnityEngine;

public class Marmu : Enemy
{
    public float bounceForce; // Fuerza de cada rebote
    public float bounceDuration = 8f; // Duración de la fase de rebote
    public float pauseDuration = 2f; // Duración de la pausa entre rebotes

    private Rigidbody2D rb;
    private bool isBouncing = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(BounceRoutine());
    }

    IEnumerator BounceRoutine()
    {
        while (true) // Bucle infinito
        {
            // Comienza a rebotar con una fuerza aleatoria
            isBouncing = true;
            ApplyRandomBounceForce();

            // Espera durante la fase de rebote
            yield return new WaitForSeconds(bounceDuration);

            // Para de rebotar
            isBouncing = false;
            rb.velocity = Vector2.zero; // Detiene el movimiento

            // Espera durante la pausa
            yield return new WaitForSeconds(pauseDuration);
        }
    }

    private void ApplyRandomBounceForce()
    {
        // Aplica un impulso inicial en una dirección aleatoria
        Vector2 forceDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.AddForce(forceDirection * bounceForce, ForceMode2D.Impulse);
    }

    // Puedes sobrescribir métodos de la clase Enemy si es necesario
    // Por ejemplo, si el enemigo debe comportarse diferente al recibir daño
}