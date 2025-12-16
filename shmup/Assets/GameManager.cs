using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int life = 0;
    public PlayerBehavior player;

    public TMP_Text scoreText;
    public TMP_Text lifeText;

    public string menuSceneName = "menuScene";
    public string playSceneName = "PlayScene";
    public string playSceneName2 = "PlayScene2";

    private bool isPaused = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (!isPaused)
        {
            if (player != null)
            {
                life = player.life;
            }

            if (scoreText != null)
                scoreText.text = "Score: " + score;

            if (lifeText != null)
                lifeText.text = "Life: " + life;

            playScene2();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            Debug.Log("JEU EN PAUSE");
        }
        else
        {
            Time.timeScale = 1f;
            Debug.Log("JEU REPRIS");
        }
    }

    public void playscene()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(playSceneName);
    }

    public void playScene2()
    {
        if (score >= 10000)
        {
            Time.timeScale = 1f; 
            SceneManager.LoadScene(playSceneName2);
            score = 0;
        }
    }
}
