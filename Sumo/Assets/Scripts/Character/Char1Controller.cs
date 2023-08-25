using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char1Controller : CharController
{
   
    void FixedUpdate()
    {
        if (justJumped)
        {
            Jump();
            justJumped = false;
        }
        
        Move();

        if (rb.transform.position.y < -5)
        {
            Die();
        }
    }

    void Update()
    {
        if (!justJumped && Input.GetKeyDown(KeyCode.UpArrow))
        {
            justJumped = true;
        }
    }

    void Jump()
    {
        if (onGround)
        {
            onGround = false;
            Vector3 jumpDirection = transform.up;
            rb.centerOfMass = new Vector3(0, 1, 0);
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(jumpDirection * jumpPower, ForceMode.Impulse);
            rb.AddForce(Vector3.up * (jumpPower / 5), ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
        }
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.AddRelativeTorque(Vector3.up * horizRotateSpeed * horizontalInput * Time.deltaTime, ForceMode.Acceleration);
        float verticalInput = Input.GetAxis("Vertical1");
        rb.AddRelativeTorque(Vector3.right * verticRotateSpeed * verticalInput * Time.deltaTime, ForceMode.Acceleration);
        if (Input.GetAxis("Horizontal") != 0)
        {
            isSpinning = true;
        }
        else
        {
            isSpinning = false;
            Vector3 normalAng = rb.angularVelocity.normalized;
            rb.AddTorque(-new Vector3(0, normalAng.y, 0) * slowDownPerSecond, ForceMode.Force);
        }
    }

    
}
