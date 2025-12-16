using UnityEngine;

public class Lazer : MonoBehaviour
{
    public float duration = 2f;      
    public float speedX = -10f;
    public int damage = 1;
    private Transform firePoint;

    void Start()
    {
        Destroy(gameObject, duration);
    }

    public void SetFirePoint(Transform fp)
    {
        firePoint = fp;
    }
    void Update()
    {

        transform.position += Vector3.right * speedX * Time.deltaTime;

        if (firePoint != null)
        {
            transform.position = new Vector3(
                transform.position.x,
                firePoint.position.y,
                transform.position.z
            );
        }
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
