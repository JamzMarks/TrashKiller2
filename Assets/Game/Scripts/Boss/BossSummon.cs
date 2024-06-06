using UnityEngine;
using Photon.Pun;
using System.Collections;

public class BossSummon : MonoBehaviour
{
    public GameObject enemyPrefab; // O prefab do inimigo a ser invocado
    public Transform[] summonPoints; // Array de pontos de invocação
    public float summonInterval = 10.0f; // Intervalo de tempo entre invocações
    private float nextSummonTime = 0f; // Tempo para a próxima invocação
    private float spawnStartTime = 90.0f; // Tempo para começar a invocar (em segundos)

    void Update()
    {
        // Verifique se o tempo de início da invocação foi alcançado
        if (Time.time >= spawnStartTime)
        {
            if (Time.time >= nextSummonTime)
            {
                StartCoroutine(SummonEnemies()); // Chama a função para invocar inimigos com a coroutine
                nextSummonTime = Time.time + summonInterval; // Atualiza o tempo para a próxima invocação
            }
        }
    }

    IEnumerator SummonEnemies()
    {
        if (summonPoints.Length < 2)
        {
            Debug.LogError("Not enough summon points set for BossSummon.");
            yield break;
        }

        for (int i = 0; i < 2; i++) // Invoca dois inimigos
        {
            int pointIndex = Random.Range(0, summonPoints.Length); // Escolhe um ponto de invocação aleatório
            GameObject enemy = PhotonNetwork.Instantiate(enemyPrefab.name, summonPoints[pointIndex].position, Quaternion.identity); // Cria o inimigo na posição do ponto de invocação
            StartCoroutine(PauseEnemy(enemy));
        }
        Debug.Log("Boss summoned 2 enemies!");
    }

    IEnumerator PauseEnemy(GameObject enemy)
    {
        // Disable the enemy's movement component
        var movementComponent = enemy.GetComponent<Platformer2D.Character.CharacterMovement2D>();
        if (movementComponent != null)
        {
            movementComponent.enabled = false;
            Debug.Log($"Disabled movement for {enemy.name}");
        }

        yield return new WaitForSeconds(3); // Espera por 3 segundos

        // Enable the enemy's movement component
        if (movementComponent != null)
        {
            movementComponent.enabled = true;
            Debug.Log($"Enabled movement for {enemy.name}");
        }
    }
}
