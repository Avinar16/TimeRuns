using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName = "Wave";
        public List<GameObject> enemies;
    }

    [Header("Wave Settings")]
    [SerializeField] private List<Wave> waves;
    [SerializeField] private float timeBetweenWaves = 3f;
    [SerializeField] private float spawnInterval = 0.5f;
    [SerializeField] private float waveSpreadRadius = 2f;
    [SerializeField]
    private int maxEntities = 20;
    [SerializeField]
    private int totalEntities;

    [Header("Player Settings")]
    [SerializeField] private Transform player;
    [SerializeField] private float minDistanceFromPlayer = 4f;
    [SerializeField] private float maxDistanceFromPlayer = 8f;
    [SerializeField] private LayerMask obstacleLayers;
    
    private void Start()
    {
        if (player == null) player = Player.instance.transform;

        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            Wave randomWave = waves[Random.Range(0, waves.Count)];
            Vector2 spawnCenter = GetValidSpawnPoint();

            if (spawnCenter != Vector2.zero)
            {
                if (totalEntities < maxEntities)
                {
                    yield return StartCoroutine(SpawnWave(randomWave.enemies, spawnCenter));
                }
            }
            else
            {
                Debug.LogWarning("Failed to find valid spawn position!");
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    private IEnumerator SpawnWave(List<GameObject> enemies, Vector2 center)
    {
        foreach (var enemy in enemies)
        {
            Vector2 spawnPos = center + Random.insideUnitCircle * waveSpreadRadius;
            GameObject spawned = Instantiate(enemy, spawnPos, Quaternion.identity);
            spawned.GetComponent<Enemy>().OnDeath += CountAsDead;
            totalEntities++;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
    private void CountAsDead()
    {
        totalEntities--;
    }
    private Vector2 GetValidSpawnPoint()
    {
        int maxAttempts = 30;
        Vector2 spawnPoint = Vector2.zero;

        for (int i = 0; i < maxAttempts; i++)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomDistance = Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
            Vector2 testPosition = (Vector2)player.position + randomDirection * randomDistance;

            if (IsPositionValid(testPosition))
            {
                spawnPoint = testPosition;
                break;
            }
        }

        return spawnPoint;
    }

    private bool IsPositionValid(Vector2 position)
    {
        // Проверка расстояния от игрока
        float distanceToPlayer = Vector2.Distance(position, player.position);
        if (distanceToPlayer < minDistanceFromPlayer) return false;

        // Проверка на препятствия (стены и т.д.)
        if (Physics2D.OverlapCircle(position, 1f, obstacleLayers))
        {
            return false;
        }

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(player.position, minDistanceFromPlayer);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(player.position, maxDistanceFromPlayer);
        }
    }
}