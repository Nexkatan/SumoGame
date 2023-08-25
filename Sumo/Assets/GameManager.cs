using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public bool hasTelePowerup = false;
    public bool hasAcidPowerup = false;
    public bool hasBouncyWallPowerup = false;

    public bool isGameActive = false;
    public bool gameOver = false;

    public PlayerController player1;
    public int player1Deaths;
    public PlayerController player2;
    public int player2Deaths;

    

    public GameObject Walls;
    public SpawnManager spawnManager;
    public int waveNum;

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

    public void Start1PlayerGame()
    {
        SceneManager.LoadScene("Level 1");
        isGameActive = true;
        gameOver = false;
    }
    public void Start2PlayerGame()
    {
        SceneManager.LoadScene("2Player");
        if (GameplayButtons != null)
        {
            GameplayButtons.SetActive(true);
        }
        isGameActive = true;
        gameOver = false;
    }

    public void Start2CharGame() 
    {
        SceneManager.LoadScene("Sumo Physics");
    }

    public void UpdateScore()
    {
        if (player1ScoreText != null)
        {
            waveNum = spawnManager.waveNumber;
            string waves = "Wave: " + waveNum;
            string lives = "Lives: " + (5 - player1.deathCount);
            string bothLines = waves + "\n" + lives;
            player1ScoreText.text = bothLines;
            if (player2ScoreText != null)
            {
                player1Deaths = player1.deathCount;
                string player1text = "Player 1";
                string deaths1 = "Deaths: " + player1Deaths;
                player1ScoreText.text = player1text + "\n" + deaths1;
                player2Deaths = player2.deathCount;
                string player2text = "Player 2";
                string deaths2 = "Deaths: " + player2Deaths;
                player2ScoreText.text = player2text + "\n" + deaths2;
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
            hasTelePowerup = false;
            hasAcidPowerup = false;
            hasBouncyWallPowerup = false;
            spawnManager.DespawnAllPowerups();
            player1.WallInlayz.SetActive(false);
            GameplayButtons.SetActive(false);
            foreach (Transform child in Walls.transform)
            {
                child.localScale = new Vector3(14.7f, 1.3f, .2f);
            }
        }
    }

    public void HomeButton()
    {
        SceneManager.LoadScene("Home");
    }
}
