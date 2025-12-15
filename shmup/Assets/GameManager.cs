using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int life = 0;
    public PlayerBehavior player;

    public TMP_Text scoreText; 
    public TMP_Text lifeText;  

    void Update()
    {

        life = player.hp;

        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (lifeText != null)
            lifeText.text = "Life: " + life;
    }
}
