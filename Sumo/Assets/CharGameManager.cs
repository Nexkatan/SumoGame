using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharGameManager : MonoBehaviour
{
    public bool isGameActive = false;
    public bool gameOver = false;

    public CharController player1;
    public int player1Deaths;
    public CharController player2;
    public int player2Deaths;

    public GameObject Walls;

    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI titleText;
    public GameObject GameplayButtons;

    private void Start()
    {
        UpdateScore();
    }
    void Update()
    {
        if (player1.deathCount == 5)
        {
            GameOver();
        }
        if (player2 != null && player2.deathCount == 5)
        {
            GameOver();
        }
    }

    public void UpdateScore()
    {
        if (player1ScoreText != null)
        {
            string lives = "Lives: " + (5 - player1.deathCount);
            player1ScoreText.text = lives;
            if (player2ScoreText != null)
            {
                player1Deaths = player1.deathCount;
                player1ScoreText.text = "Player 1 Deaths: " + player1Deaths;
                player2Deaths = player2.deathCount;
                player2ScoreText.text = "Player 2 Deaths: " + player2Deaths;
            }
        }
    }

    public void GameOver()
    {

        if (isGameActive)
        {
            Debug.Log("GameOver");
            gameOverText.gameObject.SetActive(true);
            gameOver = true;
            isGameActive = false;
            GameplayButtons.SetActive(false);
            foreach (Transform child in Walls.transform)
            {
                child.localScale = new Vector3(14.7f, 1.3f, .2f);
            }
        }
    }
}
