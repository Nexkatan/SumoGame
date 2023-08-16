using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInlaysTwoPlayer : MonoBehaviour
{
    public Player1Controller player1;
    public Player2Controller player2;


    public Material acidMat;
    public Material bouncyMat;
    public Material teleMat;

    private void Update()
    {
        if (player1.hasAcidPowerup || player2.hasAcidPowerup)
        {
            GetComponent<MeshRenderer>().material = acidMat;
        }
        if (player1.hasBouncyWallPowerup || player2.hasBouncyWallPowerup)
        {
            GetComponent<MeshRenderer>().material = bouncyMat;
        }
        if (player1.hasTelePowerup || player2.hasTelePowerup)
        {
            GetComponent<MeshRenderer>().material = teleMat;
        }
    }
}
