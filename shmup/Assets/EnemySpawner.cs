using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EnemyData
{
    public GameObject prefab;   
    public int hp = 1;          
    public float moveSpeed = 5f; 
    public float verticalSpeed = 3f;
    public BossMooving boss;
}

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuration des ennemis")]
    public EnemyData[] enemies;
    [Header("Boss")]
    public GameObject bossPrefab;
    public int bossHp = 50;
    public float bossMoveSpeed = 3f;
    public float bossVerticalSpeed = 2f;
    public Slider bossHealthSlider;
    [Header("Spawn Settings")]
    public float spawnInterval = 10f;

    private float camHalfWidth;
    private float camHalfHeight;
    private bool bossSpawned = false;

    void Start()
    {
        camHalfHeight = Camera.main.orthographicSize;
        camHalfWidth = camHalfHeight * Camera.main.aspect;
        if (bossHealthSlider != null)
        {
            bossHealthSlider.gameObject.SetActive(false); 
        }
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (GameManager.Instance.score <= 1000)
            {
                SpawnEnemy();
            }
            SpawnBoss();
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
            enemyBehavior.gameManager = GameManager.Instance; 
        }

        int moveType = Random.Range(0, 3); 
        var moveZigzag = enemy.GetComponent<EnemyMoveZigzag>();
        var moveRandom = enemy.GetComponent<EnemyMoveRandom>();
        var moveNone = enemy.GetComponent<EnemyMoveNone>();

        if (moveZigzag != null) moveZigzag.enabled = (moveType == 0);
        if (moveRandom != null) moveRandom.enabled = (moveType == 1);
        if (moveNone != null) moveNone.enabled = (moveType == 2);
    }

    void SpawnBoss()
    {
        if (SceneManager.GetActiveScene().name != "PlayScene2") return;
        if (bossSpawned) return;
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.score < 1200) return;

        Vector3 spawnPos = new Vector3(Camera.main.transform.position.x + camHalfWidth + 2f,
                                       Camera.main.transform.position.y, 0f);

        GameObject boss = Instantiate(bossPrefab, spawnPos, Quaternion.identity);
        EnemyBehavior bossBehavior = boss.GetComponent<EnemyBehavior>();

        if (bossBehavior != null)
        {

            bossBehavior.bossHealthSlider = GameManager.Instance.bossHealthSlider;
            bossBehavior.maxHp = bossHp;
            bossBehavior.currentHp = bossHp;
            bossBehavior.moveSpeed = bossMoveSpeed;
            bossBehavior.verticalSpeed = bossVerticalSpeed;
            bossBehavior.gameManager = GameManager.Instance;

        }

        bossSpawned = true;
    }




}
