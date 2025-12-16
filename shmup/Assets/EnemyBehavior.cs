using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Stats")]
    public int hp = 1;
    public int currentHp = 1;
    public int maxHp = 500;
    public float moveSpeed = 5f;
    public float verticalSpeed = 3f;
    public Slider bossHealthSlider; 
    public Vector3 sliderScreenOffset = new Vector3(0, 200f, 0);
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
        currentHp = maxHp;
        if (bossHealthSlider != null)
        {
            bossHealthSlider.gameObject.SetActive(true);
            bossHealthSlider.maxValue = maxHp;
            bossHealthSlider.value = currentHp;
        }
    }

    void Update()
    {
        Shoot();

        if (bossHealthSlider != null)
        {
            bossHealthSlider.value = currentHp;

        }
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
        if (isDead || isInvincible) return;
        

        hp -= damage;

        if (!isFlashing)
            StartCoroutine(DamageFlash());

        if (hp <= 0)
            Die();
    }
    public void BossTakeDamage(int damage)
    {
        if (isDead || isInvincible) return;

        currentHp -= damage;
        if (!isFlashing) StartCoroutine(DamageFlash());

        if (bossHealthSlider != null)
            bossHealthSlider.value = currentHp;

        if (currentHp <= 0) Die();
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

    void UpdateHealthSlider()
    {
        if (bossHealthSlider == null) return;


        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        screenPos.y = Screen.height - sliderScreenOffset.y;
        screenPos.x = Screen.width / 2; 
        bossHealthSlider.transform.position = screenPos;

        bossHealthSlider.value = hp;
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
        if (bossHealthSlider != null)
        {
            bossHealthSlider.gameObject.SetActive(false);
        }

        Destroy(gameObject);
    }


}
