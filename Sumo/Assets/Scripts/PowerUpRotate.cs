using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUpRotate : MonoBehaviour
{
    public GameObject PowerupIndicator;
    public GameObject wallInlays;


    public float degreesPS = 50;
    
    public float powerupIndicatorTime = 7;
    public bool hasPowerup;

    void Update()
    {
        transform.Rotate(new Vector3(0, degreesPS, 0 ) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopCoroutine("PowerupIndicatorCountDown");
            hasPowerup = true;
            PowerupIndicator.GetComponent<MeshRenderer>().enabled = true;

            if (gameObject.CompareTag("AcidWallPowerup") || gameObject.CompareTag("TeleportPowerup") || gameObject.CompareTag("BouncyWallPowerup"))
            {
                wallInlays.SetActive(true);
            }

            StartCoroutine("PowerupIndicatorCountDown");
        }
        
    }

    IEnumerator PowerupIndicatorCountDown()
    {
        yield return new WaitForSeconds(powerupIndicatorTime);
        PowerupIndicator.GetComponent<MeshRenderer>().enabled = false;
        hasPowerup = false;
        wallInlays.SetActive(false);
    }
}
