using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TwoPlayerPowerupController : MonoBehaviour
{
    

    public float degreesPS = 50;


    void Update()
    {
        transform.Rotate(new Vector3(0, degreesPS, 0) * Time.deltaTime);
    }

}
