using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Stats")]
    public int hp = 1;
    public float moveSpeed = 5f; 
    public float verticalSpeed = 3f; 

    [Header("Shooting")]
    public GameObject enemyBulletPrefab;
    public float fireRate = 1f;

    private float fireTimer;
    public GameManager gameManager;
    private bool isDead = false;

    void Start()
    {
        fireTimer = fireRate;
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
        if (hp <= 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
        if (gameManager != null)
            gameManager.score += 250;
        Destroy(gameObject);
    }
}

