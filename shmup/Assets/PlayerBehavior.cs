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

    public float fireRate = 0.7f; 
    private float fireTimer = 0f;

   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement = new Vector3(x, y, 0f).normalized;

        if (Input.GetKey(KeyCode.Space) && fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }
        if (fireTimer > 0f)
        {
            fireTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        Vector3 autoMove = Vector3.right * autoMoveSpeed;
        Vector3 inputMove = movement * speed;

        Vector3 newPos = rb.position + (autoMove + inputMove) * Time.fixedDeltaTime;

        newPos.y = Mathf.Clamp(newPos.y, -8f, 11f);

        float camHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float minX = Camera.main.transform.position.x - camHalfWidth -7;
        float maxX = Camera.main.transform.position.x + camHalfWidth +7;

        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);

        rb.MovePosition(newPos);
    }


    void Shoot()
    {
        Instantiate( bulletPrefab,firePoint != null ? firePoint.position : transform.position,Quaternion.identity);
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
