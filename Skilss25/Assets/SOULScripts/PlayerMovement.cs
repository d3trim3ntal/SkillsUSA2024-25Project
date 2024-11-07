using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    // Speed and jump power initialization; One to set the initial value, and the other to change during gameplay
    public float speedInit = 5;
    float currentSpeed = 5;
    public float jumpForceInit = 10;
    float jumpForce = 10;
    public float groundDrag;

    //the orientation gameobject to figure out movement direction
    Vector3 movementDirection;
    public Transform orient;

    // Adjust gravity
    public float gravityScale = 1;

    // Input handling variables
    public float horiInput;
    public float vertInput;

    // Grounded or not
    public bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        currentSpeed = speedInit;
        jumpForce = jumpForceInit;
    }

    // Update is called once per frame
    void Update()
    {
        //make drag if the player is grounded
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
        
        // Get inputs
        horiInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");
        // Keep player horizontal velocity controlled and consistent
        rb.velocity = Vector3.zero + Vector3.up * rb.velocity.y;
        //get move direction and move 
        movementDirection = orient.forward * vertInput + orient.right * horiInput;
        rb.AddForce(movementDirection.normalized * speedInit * 10, ForceMode.Force);
        //limit velocity
        Vector3 flat = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(flat.magnitude > speedInit)
        {
            Vector3 limitedVelocity = flat.normalized * speedInit;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }


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
