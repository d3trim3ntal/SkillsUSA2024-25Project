using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    // Speed and jump power initialization; One to set the initial value, and the other to change during gameplay
    public float speedInit = 5;
    float currentSpeed = 5;
    public float jumpForceInit = 10;
    float jumpForce = 10;

    // Camera turning speed
    public float rotationSpeed = 240;

    // Adjust gravity
    public float gravityScale = 1;

    // Input handling variables
    public float moveDirection;
    public float rotationDirection;

    // Grounded or not
    public bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = speedInit;
        jumpForce = jumpForceInit;
    }

    // Update is called once per frame
    void Update()
    {
        // Get inputs
        moveDirection = Input.GetAxisRaw("Vertical");
        rotationDirection = Input.GetAxisRaw("Horizontal");
        // Keep player horizontal velocity controlled and consistent
        rb.velocity = Vector3.zero + Vector3.up * rb.velocity.y;
        rb.AddRelativeForce(Vector3.forward * moveDirection * currentSpeed, ForceMode.VelocityChange);
        // Rotate camera
        transform.Rotate(rotationDirection * rotationSpeed * Time.deltaTime * Vector3.up);
        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddRelativeForce(Vector3.up * jumpForce, ForceMode.Impulse);
            grounded = false;
        }
        // Adding more down force for gravity
        if (!grounded)
        {
            rb.AddRelativeForce(Physics.gravity * (gravityScale - 1));
        }
    }

    void OnCollisionStay(Collision c)
    {
        // Check collision object tag
        if (c.gameObject.CompareTag("Ground") || c.gameObject.CompareTag("Platform") || c.gameObject.CompareTag("Robot"))
        {
            // Check whether or not the player is falling/moving up
            if (Mathf.Abs(rb.velocity.y) < 0.05f)
            {
                grounded = true;
                // Make player follow platform or robot while it moves
                if (c.gameObject.CompareTag("Platform") || c.gameObject.CompareTag("Robot"))
                {
                    transform.parent = c.gameObject.transform;
                }
            }
        }
    }

    void OnCollisionExit(Collision c)
    {
        if (c.gameObject.CompareTag("Ground") || c.gameObject.CompareTag("Platform") || c.gameObject.CompareTag("Robot"))
        {
            grounded = false;
            transform.parent = null;
        }
    }
}
