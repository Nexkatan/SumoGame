using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpIndicator : MonoBehaviour
{
    public GameObject player;
    public float degreesPS = 50;
    public Vector3 offset = new Vector3 (0, 3, 0);
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}