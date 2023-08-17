using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private Rigidbody rb1;
    public PlayerController player2;
    public int player2Deaths;
    private Rigidbody rb2;


    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI titleText;

    private void Start()
    {
        rb1 = player1.GetComponent<Rigidbody>();
        rb2 = player2.GetComponent<Rigidbody>();
        UpdateScore();
    }
    void Update()
    {
        if (player1.deathCount > 4)
        {
            GameOver();
        }
        if (player2.deathCount > 4)
        {
            GameOver();
        }
    }

    public void StartGame()
    {
        UpdateScore();
        player1.deathCount = 0;
        player2.deathCount = 0;
        rb1.angularVelocity = Vector3.zero;
        rb1.velocity = Vector3.zero;
        rb1.transform.position = new Vector3(5, 2, 0);
        rb2.angularVelocity = Vector3.zero;
        rb2.velocity = Vector3.zero;
        rb2.transform.position = new Vector3(-5, 2, 0);
        titleText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        isGameActive = true;
    }

    public void UpdateScore()
    {
        player1Deaths = player1.deathCount;
        player2Deaths = player2.deathCount;
        player1ScoreText.text = "Player 1 Deaths: " + player1Deaths;
        player2ScoreText.text = "Player 2 Deaths: " + player2Deaths;
    }

    public void GameOver()
    {
        if (isGameActive) 
        { 
        gameOverText.gameObject.SetActive(true);
        gameOver = true;
        isGameActive = false;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
