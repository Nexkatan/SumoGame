using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player2Controller : PlayerController
{
    private Rigidbody player2Rb;
    public GameObject focalPoint;

    private void Start()
    {
        focalPoint = GameObject.Find("Focal Point");
        player2Rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical3");
        player2Rb.AddForce(focalPoint.transform.forward * moveSpeed * verticalInput * doubleMassSpeed * Time.deltaTime);
        float horizontalInput = Input.GetAxis("Horizontal2");
        player2Rb.AddForce(focalPoint.transform.right * moveSpeed * horizontalInput * doubleMassSpeed * Time.deltaTime);
       
    }
}
