using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 10f;
    public float autoMoveSpeed = 5f;

    private Rigidbody rb;
    private Vector3 movement;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public int life = 3;
    public int hp = 1;
    
    public float fireRate = 0.7f;
    private float fireTimer = 0f;
    [Header("Cheats / Debug")]
    public bool invulnerable = false; 

    [Header("Respawn Settings")]
    public Vector3 respawnOffset = new Vector3(-10f, 0f, 0f);
    public float invincibleTime = 2f; 
    private bool isInvincible = false;
    [Header("Shooting")]
    public GameObject missilePrefab; 
    [Header("PowerUps")]
    public bool hasShoot2 = false;
    public bool hasShoot3 = false;

    public GameManager gameManager; 
    public GameObject bulletPrefab2; 
    public float shoot2Angle = 3f;

    private Renderer rend;

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.player = this;
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement = new Vector3(x, y, 0f).normalized;

        if (Input.GetKey(KeyCode.Space) && fireTimer <= 0f)
        {
            Shoot(); 

            if (hasShoot2)
                Shoot2();

            if (hasShoot3)
                Shoot3();

            fireTimer = fireRate;
        }
        if (fireTimer > 0f)
            fireTimer -= Time.deltaTime;
    }


    void FixedUpdate()
    {
        Vector3 autoMove = Vector3.right * autoMoveSpeed;
        Vector3 inputMove = movement * speed;

        Vector3 newPos = rb.position + (autoMove + inputMove) * Time.fixedDeltaTime;

        newPos.y = Mathf.Clamp(newPos.y, -8f, 11f);

        float camHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float minX = Camera.main.transform.position.x - camHalfWidth - 7;
        float maxX = Camera.main.transform.position.x + camHalfWidth + 7;

        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);

        rb.MovePosition(newPos);
    }

    void Shoot()
    {
       
        Instantiate(bulletPrefab, firePoint != null ? firePoint.position : transform.position, Quaternion.identity);
    }

    void Shoot2()
    {
        if (fireTimer > 0f) return; 
        fireTimer = fireRate;

        Vector3 topFirePos = transform.position + Vector3.up * 1f;   
        Vector3 bottomFirePos = transform.position + Vector3.down * 1f;

        Vector3 topDir = Quaternion.Euler(0, 0, -shoot2Angle) * Vector3.right;  
        Vector3 bottomDir = Quaternion.Euler(0, 0, shoot2Angle) * Vector3.right; 

        GameObject topBullet = Instantiate(bulletPrefab2, topFirePos, Quaternion.identity);
        topBullet.transform.right = topDir;

        GameObject bottomBullet = Instantiate(bulletPrefab2, bottomFirePos, Quaternion.identity);
        bottomBullet.transform.right = bottomDir;
    }
    void Shoot3()
    {
        if (missilePrefab == null) return;

        Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position;
        Instantiate(missilePrefab, spawnPos, Quaternion.identity);
    }


    public void PTakeDamage(int damage)
    {
        if (isInvincible || invulnerable) return;

        hp -= damage;

        if (hp <= 0)
        {
            StartCoroutine(Respawn());
        }
    }


    private IEnumerator Respawn()
    {
        isInvincible = true;


        Vector3 camPos = Camera.main.transform.position;
        Vector3 respawnPos = new Vector3(camPos.x + respawnOffset.x, respawnOffset.y, 0f);
        rb.position = respawnPos;
        life -= 1;
        float timer = 0f;
        while (timer < invincibleTime)
        {
            rend.enabled = !rend.enabled;
            timer += 0.2f;
            yield return new WaitForSeconds(0.2f);
        }
        rend.enabled = true;
        isInvincible = false;

        hp = 1;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp1"))
        {
            hasShoot2 = true;
            Destroy(other.gameObject);
            GameManager.Instance.score += 250;
        }

        if (other.CompareTag("PowerUp2"))
        {
            hasShoot3 = true;
            Destroy(other.gameObject);
            GameManager.Instance.score = 250;
        }
    }

}
