using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int hp = 1;

    [Header("Movement")]
    public float speed = 3f;
    public float topY = 7f;
    public float bottomY = -7f;

    [Header("Shooting")]
    public GameObject enemyBulletPrefab;
    public float fireRate = 1f;

    private float fireTimer;
    private bool goingDown = true;

    void Start()
    {
        fireTimer = fireRate;
    }

    void Update()
    {
        Move();
        Shoot();
        Moving(5.0f);
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Moving(float moveSpeed)
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    void Move()
    {
        float direction = goingDown ? -1f : 1f;
        transform.Translate(Vector3.up * direction * speed * Time.deltaTime);

        if (transform.position.y <= bottomY)
            goingDown = false;

        if (transform.position.y >= topY)
            goingDown = true;
    }

    void Shoot()
    {
        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0f)
        {
            Instantiate(
                enemyBulletPrefab,
                transform.position,
                Quaternion.identity
            );

            fireTimer = fireRate;
        }
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
