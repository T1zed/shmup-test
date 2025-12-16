using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Stats")]
    public int hp = 1;
    public float moveSpeed = 5f;
    public float verticalSpeed = 3f;

    [Header("Shooting")]
    public GameObject enemyBulletPrefab;
    public float fireRate = 1f;

    [Header("Spawn Invincibility")]
    public float spawnInvincibleTime = 0.5f;

    [Header("Damage Flash")]
    public float flashDuration = 0.8f;
    public float flashInterval = 0.2f; 

    private float fireTimer;
    public GameManager gameManager;
    private bool isDead = false;
    private bool isFlashing = false;
    private bool isInvincible = true;
    private Renderer rend;

    void Start()
    {
        fireTimer = fireRate;
        rend = GetComponent<Renderer>();
        StartCoroutine(SpawnInvincibility());
    }

    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            fireTimer = fireRate;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        hp -= damage;

        if (!isFlashing)
            StartCoroutine(DamageFlash());

        if (hp <= 0)
            Die();
    }

    private IEnumerator DamageFlash()
    {
        isFlashing = true;
        float timer = 0f;

        while (timer < flashDuration)
        {
            if (rend != null)
                rend.enabled = !rend.enabled;

            timer += flashInterval;
            yield return new WaitForSeconds(flashInterval);
        }

        if (rend != null)
            rend.enabled = true;

        isFlashing = false;
    }
    private IEnumerator SpawnInvincibility()
    {
        float timer = 0f;

        while (timer < spawnInvincibleTime)
        {
            if (rend != null)
                rend.enabled = !rend.enabled; 

            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }

        if (rend != null)
            rend.enabled = true;

        isInvincible = false;
    }
    private void Die()
    {
        isDead = true;
        if (gameManager != null)
            gameManager.score += 250;

        Destroy(gameObject);
    }
}
