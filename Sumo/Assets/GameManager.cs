using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool hasTelePowerup = false;
    public bool hasAcidPowerup = false;
    public bool hasBouncyWallPowerup = false;

    public bool gameOver = false;

    public PlayerController player1;
    public PlayerController player2;

    void Update()
    {
        if (player1.deathCount > 4)
        {
            gameOver = true;
            Debug.Log("Player 2 wins");
        }
        if (player2.deathCount > 4)
        {
            gameOver = true;
            Debug.Log("Player 1 wins");
        }
    }

}
