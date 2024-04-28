using System.Collections;
using UnityEngine;

public class SpawnObjectAction : Action
{
    private GameObject objectToSpawn;
    private Transform enemyTransform;
    private MonoBehaviour context; // Para usar StartCoroutine
    private float delay;
    public SpawnObjectAction(GameObject objectToSpawn, Transform enemyTransform)
    {
        this.objectToSpawn = objectToSpawn;
        this.enemyTransform = enemyTransform;
        this.delay = 2;
    }

    public override void Execute(Enemy enemy)
    {
        // Posiciones relativas: 4 a la izquierda, 2 a la izquierda, 2 a la derecha, 4 a la derecha
        float[] relativePositions = new float[] {-4, -2, 2, 4};

        foreach (float position in relativePositions)
        {
            Vector3 spawnPosition = enemyTransform.position + new Vector3(position, 0, 0); // Ajustar la coordenada Y si es necesario
            GameObject swordInstance = Object.Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            context.StartCoroutine(TriggerAttack(swordInstance, delay)); // Espera 2 segundos antes de atacar
            delay += 2;
        }
    }

    private IEnumerator TriggerAttack(GameObject sword, float delaySword)
    {
        yield return new WaitForSeconds(delaySword);
        sword.GetComponent<Sword>().Attack();
    }
}