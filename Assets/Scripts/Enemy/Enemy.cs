using UnityEngine;

public class Enemy : MonoBehaviour
{
    private State currentState;
    public int maxHealth = 100; // Máxima salud del enemigo
    int currentHealth; 
    
    void Start()
    {
        currentHealth = maxHealth; // Inicializar la salud del enemigo
        ChangeState(new IdleState(this));
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.Execute();
        }
    }

    public void ChangeState(State newState)
    {
        if (currentState != null)
            currentState.Exit();
        currentState = newState;
        currentState.Enter();

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
