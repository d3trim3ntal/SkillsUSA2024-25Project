using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator adaAni;
    Rigidbody rb;

    // Speed and jump power initialization; One to set the initial value, and the other to change during gameplay
    [Header("Stats")]
    public float speedInit = 5;
    public float currentSpeed = 5;
    public float jumpForceInit = 10;
    public float jumpForce = 10;
    public float groundDrag;
    public float airMultiplier = .4f;

    //the orientation gameobject to figure out movement direction
    [Header("Misc")]
    Vector3 movementDirection;
    public Transform orient;
    [SerializeField] BoxCollider boxCollider;
    private bool movable = true;
    private CinemachineFreeLook followCam;

    // Input handling variables
    public float horiInput;
    public float vertInput;

    // Raycast and grounded variables
    [Header("Ground Check Vars")]
    public bool grounded;
    public bool onPlatform;
    private bool jumpable = true;
    public float raycastDist = 2f;
    public float jumpCooldown = 1f;
    public LayerMask ground;

    


    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        currentSpeed = speedInit;
        jumpForce = jumpForceInit;
        followCam = FindObjectOfType<CinemachineFreeLook>();
    }
    void Update()
    {
        //Cursor keybind
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (movable)
            {
                followCam.enabled = false;
                movable = false;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                followCam.enabled = true;

                movable = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        // Get inputs
        horiInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");
        //grounded = Physics.Raycast(transform.position, Vector3.down, RaycastDist, ground);
        Vector3 extents = new Vector3(10f, 10f, 10f);
        Vector3 boxPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        grounded = Physics.BoxCast(boxPos - (transform.localScale.y * GetComponent<BoxCollider>().size.y * 0.5f - 0.25f) * Vector3.up, new Vector3(boxCollider.size.x, 0, boxCollider.size.z), Vector3.down, transform.rotation, raycastDist, ground);
        //make drag if the player is grounded
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
        //jump
        if (Input.GetKey(KeyCode.Space) && grounded && jumpable)
        {
            Debug.Log("Jump");
            jumpable = false;
            Jump();
            //start jump cooldown
            Invoke(nameof(JumpTimer), jumpCooldown);
        }
    }
    private void FixedUpdate()
    {
        

        //get move direction and move 
        movementDirection = orient.forward * vertInput + orient.right * horiInput;

        if(adaAni != null)
        {
            if (movementDirection.magnitude > 0)
            {
                adaAni.SetTrigger("Walking");
                adaAni.ResetTrigger("Still");
            }
            else
            {
                adaAni.ResetTrigger("Walking");
                adaAni.SetTrigger("Still");
            }
        }
        

        if (grounded)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            rb.AddForce(movementDirection.normalized * currentSpeed, ForceMode.VelocityChange);
        }
        else
        {
            
            movementDirection.Normalize();
            rb.velocity = new Vector3 (movementDirection.x * currentSpeed * 10 * airMultiplier, rb.velocity.y - 0.12f, movementDirection.z * currentSpeed * 10 * airMultiplier);
        }



        //limit velocity
        Vector3 flat = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flat.magnitude > speedInit)
        {
            Vector3 limitedVelocity = flat.normalized * speedInit;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }

    }
    
    
   void OnCollisionStay(Collision c)
   {
        // Check whether or not the player is falling/moving up

        if (Mathf.Abs(rb.velocity.y) < 0.05f)
        {
           
            // Make player follow platform or robot while it moves
            if (c.gameObject.CompareTag("Platform") || c.gameObject.CompareTag("Robot"))
            {
                transform.parent = c.gameObject.transform;
                onPlatform = true;
            }
            
        }
       
   }

   void OnCollisionExit(Collision c)
   {
       if (c.gameObject.CompareTag("Ground") || c.gameObject.CompareTag("Platform") || c.gameObject.CompareTag("Robot"))
       {
           transform.parent = null;
           onPlatform = false;
       }
      
   }
       

    void Jump()
    {
        rb.velocity = new(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    //Give a cooldown to the players jump
    void JumpTimer()
    {
        jumpable = true;
    }

   
}
