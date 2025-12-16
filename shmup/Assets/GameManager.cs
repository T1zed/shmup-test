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
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
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

    public void playscene()
    {
            SceneManager.LoadScene(playSceneName);
    }

    public void playScene2()
    {
        if (score >= 10000)
        {
            SceneManager.LoadScene(playSceneName2);
            score = 0;
        }
    }
}
