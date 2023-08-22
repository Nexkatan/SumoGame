using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject com;

    public float gravityMod;
    public float jumpPower;

    public float angularBound = 85;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = com.transform.position;
        Physics.gravity *= gravityMod;

    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * jumpPower, ForceMode.Impulse);
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                    
        }
    }
}
