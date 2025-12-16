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
    }

    public void playscene()
    {
            SceneManager.LoadScene(playSceneName);

    }
}
