using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char2Controller : CharController
{
    void Update()
    {
        Jump();
        Move();
        if (rb.transform.position.y < -2)
            {
                Die();
            }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.AddRelativeForce(Vector3.up * jumpPower, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
        }
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal2");
        rb.AddRelativeTorque(Vector3.up * horizRotateSpeed * horizontalInput * Time.deltaTime, ForceMode.Impulse);
        float verticalInput = Input.GetAxis("Vertical2");
        rb.AddRelativeTorque(Vector3.right * verticRotateSpeed * verticalInput * Time.deltaTime, ForceMode.Impulse);
    }

}
