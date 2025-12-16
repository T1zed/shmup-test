using UnityEngine;

public class PBullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 4f;
    public int damage = 1; 

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
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
        if (other.CompareTag("Boss"))
        {
            EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();

            if (enemy != null)
            {
                enemy.BossTakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
