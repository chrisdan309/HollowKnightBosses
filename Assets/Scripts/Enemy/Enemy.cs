using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100; // Máxima salud del enemigo
    int currentHealth; // Salud actual del enemigo
    
    void Start()
    {
        currentHealth = maxHealth; // Inicializar la salud del enemigo
    }

    public void TakeDamage(int damage)
    {
        // Restar daño a la salud
        currentHealth -= damage;

        // Si la salud llega a cero, 'matar' al enemigo
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Aquí puedes poner la lógica para lo que sucede cuando el enemigo muere
        // Por ejemplo, podrías reproducir una animación de muerte

        Debug.Log("Enemy died!");

        // Destruir el GameObject enemigo
        Destroy(gameObject);
    }

    
}
