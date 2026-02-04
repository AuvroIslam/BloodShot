using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnOffset = 2f;

    [Header("Spawn Rate Progression")]
    [Tooltip("Every X seconds the spawnInterval will be reduced by spawnIntervalDecrease (down to minSpawnInterval).")]
    [SerializeField] private float spawnIncreaseInterval = 30f;
    [Tooltip("Amount to decrease spawnInterval each increase step.")]
    [SerializeField] private float spawnIntervalDecrease = 0.2f;
    [Tooltip("Lower bound for spawnInterval.")]
    [SerializeField] private float minSpawnInterval = 0.25f;

    private Camera cam;
    private float timer;
    private float increaseTimer;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        float dt = Time.deltaTime;
        timer += dt;
        increaseTimer += dt;

        // Spawn logic
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }

        // Increase spawn rate (i.e. reduce spawnInterval) after set amount of time
        if (increaseTimer >= spawnIncreaseInterval)
        {
            // Reduce spawn interval but don't go below minimum
            spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - spawnIntervalDecrease);
            increaseTimer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPos = GetRandomPointOutsideCamera();
        Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)], spawnPos, Quaternion.identity);
    }

    private Vector2 GetRandomPointOutsideCamera()
    {
        float camHeight = cam.orthographicSize * 2f;
        float camWidth = camHeight * cam.aspect;

        Vector2 camCenter = cam.transform.position;

        // Pick a random angle around the camera
        float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2f);
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;

        // Find the half-extents of the camera rectangle
        float halfWidth = camWidth * 0.5f;
        float halfHeight = camHeight * 0.5f;

        // Scale direction so it reaches the camera edge
        float scale = Mathf.Min(
            halfWidth / Mathf.Abs(dir.x),
            halfHeight / Mathf.Abs(dir.y)
        );

        Vector2 edgePoint = camCenter + dir * scale;

        // Push slightly outside camera view
        return edgePoint + dir * spawnOffset;
    }
}

