using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 10f;
    public float autoMoveSpeed = 5f;

    private Rigidbody rb;
    private Vector3 movement;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public int hp = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement = new Vector3(x, y, 0f).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        Vector3 autoMove = Vector3.right * autoMoveSpeed;
        Vector3 inputMove = movement * speed;

        rb.MovePosition(
            rb.position + (autoMove + inputMove) * Time.fixedDeltaTime
        );
    }

    void Shoot()
    {
        Instantiate(
            bulletPrefab,
            firePoint != null ? firePoint.position : transform.position,
            Quaternion.identity
        );
    }

    public void PTakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
