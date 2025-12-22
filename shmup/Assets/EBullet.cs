using UnityEngine;
public class EBullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 5f;
    public int damage = 1;

    void Start()
    {

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBehavior player = other.GetComponent<PlayerBehavior>();

            if (player != null)
            {
                player.PTakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}