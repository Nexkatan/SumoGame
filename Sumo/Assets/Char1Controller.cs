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

        if (rb.transform.position.y < -2)
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
            rb.centerOfMass = new Vector3(0, 1, 0);
            rb.AddRelativeForce(Vector3.up * jumpPower, ForceMode.Impulse);
            rb.AddForce(Vector3.up * (jumpPower / 5), ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
        }
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.AddRelativeTorque(Vector3.up * horizRotateSpeed * horizontalInput * Time.deltaTime, ForceMode.Impulse);
        float verticalInput = 0;
        if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            verticalInput++;
            Debug.Log(verticalInput);
            rb.AddRelativeTorque(-Vector3.up * Time.deltaTime, ForceMode.Impulse);
        }
    }

}
