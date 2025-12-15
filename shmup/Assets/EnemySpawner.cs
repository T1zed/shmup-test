using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Ennemis à spawn")]
    public GameObject[] enemyPrefabs; 
    public GameManager gameManager;

    [Header("Spawn Settings")]
    public float spawnInterval = 10f; 
    private float camHalfWidth;
    private float camHalfHeight;

    void Start()
    {

        camHalfHeight = Camera.main.orthographicSize;
        camHalfWidth = camHalfHeight * Camera.main.aspect;
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0) return;
        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        float minX = Camera.main.transform.position.x;
        float maxX = Camera.main.transform.position.x + camHalfWidth;

        float minY = Camera.main.transform.position.y - camHalfHeight;
        float maxY = Camera.main.transform.position.y + camHalfHeight;

        Vector3 spawnPos = new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            0f
        );

        GameObject enemy = Instantiate(prefab, spawnPos, Quaternion.identity);

        EnemyBehavior enemyBehavior = enemy.GetComponent<EnemyBehavior>();
        if (enemyBehavior != null)
        {
            enemyBehavior.gameManager = gameManager;

        }


        int moveType = Random.Range(0, 3); 
        var moveZigzag = enemy.GetComponent<EnemyMoveZigzag>();
        var moveRandom = enemy.GetComponent<EnemyMoveRandom>();
        var moveNone = enemy.GetComponent<EnemyMoveNone>();

        if (moveZigzag != null) moveZigzag.enabled = (moveType == 0);
        if (moveRandom != null) moveRandom.enabled = (moveType == 1);
        if (moveNone != null) moveNone.enabled = (moveType == 2);
    }
}

