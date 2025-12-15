using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 6f;
    public int damage = 2;

    private Transform target;

    void Start()
    {

        Destroy(gameObject, lifeTime);

        EnemyBehavior[] enemies = Object.FindObjectsByType<EnemyBehavior>(FindObjectsSortMode.None);
        if (enemies.Length > 0)
        {

            target = enemies[Random.Range(0, enemies.Length)].transform;
        }
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;
        transform.right = direction;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}

