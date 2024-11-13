using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grounding : MonoBehaviour
{
    public PlayerMovement movement;
    public GameObject player;
    // private Rigidbody rb;
    void OnCollisionStay(Collision c)
    {
        // Check collision object tag
        if (c.gameObject.CompareTag("Ground") || c.gameObject.CompareTag("Platform") || c.gameObject.CompareTag("Robot"))
        {
            // Check whether or not the player is falling/moving up
            //if (Mathf.Abs(movement.rb.velocity.y) < 0.05f)
            //{
                //movement.grounded = true;
                // Make player follow platform or robot while it moves
                //if (c.gameObject.CompareTag("Platform") || c.gameObject.CompareTag("Robot"))
                //{
                    //player.transform.parent = c.gameObject.transform;
                //}
            //}
        }
    }

    void OnCollisionExit(Collision c)
    {
        if (c.gameObject.CompareTag("Ground") || c.gameObject.CompareTag("Platform") || c.gameObject.CompareTag("Robot"))
        {
            movement.grounded = false;
            player.transform.parent = null;
        }
    }
}
