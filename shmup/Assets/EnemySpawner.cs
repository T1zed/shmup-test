using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnemyData
{
    public GameObject prefab;   
    public int hp = 1;          
    public float moveSpeed = 5f; 
    public float verticalSpeed = 3f; 
}

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuration des ennemis")]
    public EnemyData[] enemies;

    [Header("Spawn Settings")]
    public float spawnInterval = 10f; 

    private float camHalfWidth;
    private float camHalfHeight;
    public GameManager gameManager;
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
        if (enemies.Length == 0) return;


        EnemyData data = enemies[Random.Range(0, enemies.Length)];


        float minX = Camera.main.transform.position.x;
        float maxX = Camera.main.transform.position.x + camHalfWidth;
        float minY = Camera.main.transform.position.y - camHalfHeight;
        float maxY = Camera.main.transform.position.y + camHalfHeight;

        Vector3 spawnPos = new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            0f
        );

        GameObject enemy = Instantiate(data.prefab, spawnPos, Quaternion.identity);

        EnemyBehavior enemyBehavior = enemy.GetComponent<EnemyBehavior>();
        if (enemyBehavior != null)
        {
            enemyBehavior.hp = data.hp;
            enemyBehavior.moveSpeed = data.moveSpeed;
            enemyBehavior.verticalSpeed = data.verticalSpeed;
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
