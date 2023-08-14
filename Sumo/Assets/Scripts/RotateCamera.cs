using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotateSpeed = 10;
    private Rigidbody focalRb;

    // Start is called before the first frame update
    void Start()
    {
        focalRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(-Vector3.up, rotateSpeed * Time.deltaTime);
        }
        
    }
}
