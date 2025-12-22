using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public int life = 3;
    public PlayerBehavior player;

    public GameObject uiCanvasPrefab;
    private GameObject uiCanvasInstance;
    private TMP_Text scoreText;
    private TMP_Text lifeText;
    public Slider bossHealthSlider;
    public string menuSceneName = "menuScene";
    public string playSceneName = "PlayScene";
    public string playSceneName2 = "PlayScene2";
    public string gameover = "GameOver";
    public string victory = "Victory";
    private int timeScaleIndex = 0;
    private readonly float[] timeScales = { 1f, 2f, 4f };

    private bool isPaused = false;
    private bool isGameEnding = false;

    public void LoadVictory()
    {
        if (isGameEnding) return;
        isGameEnding = true;
        SceneManager.LoadScene(victory);
    }

    public void LoadGameOver()
    {
        if (isGameEnding) return;
        isGameEnding = true;
        SceneManager.LoadScene(gameover);
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (uiCanvasPrefab != null)
            {
                uiCanvasInstance = Instantiate(uiCanvasPrefab);
                DontDestroyOnLoad(uiCanvasInstance);
                bossHealthSlider = uiCanvasInstance.transform.Find("boss")?.GetComponent<Slider>();
                if (bossHealthSlider != null)
                    bossHealthSlider.gameObject.SetActive(false);
            }


            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindWithTag("Player")?.GetComponent<PlayerBehavior>();

        if (uiCanvasInstance != null)
        {
            scoreText = uiCanvasInstance.transform.Find("Score")?.GetComponent<TMP_Text>();
            lifeText = uiCanvasInstance.transform.Find("Life")?.GetComponent<TMP_Text>();
        }
    }
    public void AddLife(int amount)
    {
        if (player == null) return;

        player.life += amount;
        life = player.life;

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();

        if (!isPaused && !isGameEnding)
        {
            if (player != null)
                life = player.life;

            if (scoreText != null)
                scoreText.text = "Score: " + score;

            if (lifeText != null)
                lifeText.text = "Life: " + life;

            string sceneName = SceneManager.GetActiveScene().name;
            if (sceneName != victory && sceneName != gameover)
                CheckPlayScene2();

            if (Input.GetKeyDown(KeyCode.F5))
            {
                AddLife(1);
            }

            if (Input.GetKeyDown(KeyCode.F6))
            {
                ToggleShoots();
            }

            if (Input.GetKeyDown(KeyCode.F7))
            {
                ToggleTimeScale();
            }

            if (Input.GetKeyDown(KeyCode.F8))
            {
                ToggleInvulnerability();
            }


        }

        if (!isGameEnding && life <= 0 && SceneManager.GetActiveScene().name != menuSceneName)
        {
            LoadGameOver();
        }
    }

    private void ToggleInvulnerability()
    {
        if (player == null) return;

        player.invulnerable = !player.invulnerable;

        Debug.Log("Invulnerable : " + player.invulnerable);
    }


    public void ToggleShoots()
    {
        if (player == null) return;

        if (!player.hasShoot2 && !player.hasShoot3)
        {
            player.hasShoot2 = true;
            player.hasShoot3 = false;
            Debug.Log("shoot2");
            return;
        }

        if (player.hasShoot2 && !player.hasShoot3)
        {
            player.hasShoot2 = true;
            player.hasShoot3 = true;
            Debug.Log("shoot3");
            return;
        }

        if (player.hasShoot2 && player.hasShoot3)
        {
            player.hasShoot2 = false;
            player.hasShoot3 = false;
            return;
        }
    }


    private void ToggleTimeScale()
    {
        timeScaleIndex = (timeScaleIndex + 1) % timeScales.Length;
        Time.timeScale = timeScales[timeScaleIndex];
        Debug.Log("TimeScale x" + timeScales[timeScaleIndex]);
    }

    public void Retry()
    {
        isGameEnding = false; 
        PlayScene1();
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        Debug.Log(isPaused ? "JEU EN PAUSE" : "JEU REPRIS");
    }

    public void PlayScene1()
    {
        Time.timeScale = 1f;
        score = 0;
        life = 3;

        player = null;

        if (uiCanvasInstance != null)
        {
            scoreText = uiCanvasInstance.transform.Find("Score")?.GetComponent<TMP_Text>();
            lifeText = uiCanvasInstance.transform.Find("Life")?.GetComponent<TMP_Text>();
        }
        SceneManager.LoadScene(Instance.playSceneName);
    }


    private void CheckPlayScene2()
    {
        if (score >= 1000 && SceneManager.GetActiveScene().name != playSceneName2)
        {
            scoreText = null;
            lifeText = null;

            if (uiCanvasInstance != null)
            {
                scoreText = uiCanvasInstance.transform.Find("Score")?.GetComponent<TMP_Text>();
                lifeText = uiCanvasInstance.transform.Find("Life")?.GetComponent<TMP_Text>();
            }

            Time.timeScale = 1f;
            score = 0;
            SceneManager.LoadScene(playSceneName2);
        }
    }
    
}
