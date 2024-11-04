using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    PlayerMovement playerMoveScript;

    // Variables for holding objects
    public GameObject objectHolding = null;
    public float pickupRadius;
    public bool currentlyHolding;

    // Variables for fixing robots
    public GameObject currentRobot = null;
    public bool currentlyFixing;
    // Start is called before the first frame update
    void Start()
    {
        playerMoveScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // Press E to pick up and drop things
        if (Input.GetKeyDown(KeyCode.E) && !currentlyFixing)
        {
            if (!currentlyHolding && playerMoveScript.grounded)
            {
                // Detects all pickups within range and essentially chooses one at random to pick up
                GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
                foreach (GameObject p in pickups)
                {
                    if ((transform.position - p.transform.position).magnitude < pickupRadius)
                    {
                        objectHolding = p;
                    }
                }
                objectHolding.GetComponent<PickupScript>().PickedUp(gameObject);
                currentlyHolding = true;
            }
            else
            {
                objectHolding.GetComponent<PickupScript>().Dropped(gameObject);
                currentlyHolding = false;
            }
        }
    }
}
