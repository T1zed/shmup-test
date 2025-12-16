using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public int life = 0;
    public PlayerBehavior player;

    public GameObject uiCanvasPrefab;
    private GameObject uiCanvasInstance;
    private TMP_Text scoreText;
    private TMP_Text lifeText;
    public Slider bossHealthSlider;
    public string menuSceneName = "menuScene";
    public string playSceneName = "PlayScene";
    public string playSceneName2 = "PlayScene2";

    private bool isPaused = false;

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

        if (uiCanvasInstance != null)
        {
            scoreText = uiCanvasInstance.transform.Find("Score")?.GetComponent<TMP_Text>();
            lifeText = uiCanvasInstance.transform.Find("Life")?.GetComponent<TMP_Text>();
        }

        player = GameObject.FindWithTag("Player")?.GetComponent<PlayerBehavior>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();

        if (!isPaused)
        {
            if (player != null)
                life = player.life;

            if (scoreText != null)
                scoreText.text = "Score: " + score;

            if (lifeText != null)
                lifeText.text = "Life: " + life;

            CheckPlayScene2();
        }
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
        SceneManager.LoadScene(playSceneName);
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
