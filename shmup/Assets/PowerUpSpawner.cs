using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs;
    public float spawnInterval = 4f;

    public float minY = -8f;
    public float maxY = 8f;

    public PlayerBehavior player;  
    public float xOffset = 30f;

    void Start()
    {
        Debug.Log("PowerUpSpawner started");
        StartCoroutine(SpawnRoutine());
    }


    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnPowerUp();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnPowerUp()
    {
        if (powerUpPrefabs.Length == 0 || player == null) return;

        float spawnX = Camera.main.transform.position.x + 10f;

        float spawnY = Random.Range(minY, maxY);

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);

        GameObject prefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
