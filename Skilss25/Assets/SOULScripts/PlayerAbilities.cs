using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    PlayerMovement playerMoveScript;
    public GameObject playerModel;

    // Variables for holding objects
    public  GameObject objectHolding = null;
    public float pickupRadius;
    public bool currentlyHolding;

    // Variables for fixing robots
    public GameObject currentRobot = null;
    public bool currentlyFixing;
    // TEMPORARY
    public GameObject robotUI;

    // Variables for interaction
    public float interactRadius;
    // Start is called before the first frame update
    void Start()
    {
        playerMoveScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        PickupFunctionality();
        InteractFunctionality();
        RobotFunctionality();
    }

    void PickupFunctionality()
    {
        GameObject[] picks = GameObject.FindGameObjectsWithTag("Pickup");
        GameObject[] item = GameObject.FindGameObjectsWithTag("Item");

        List<GameObject> totalPicks = new(picks.Length + item.Length);

        foreach (GameObject p in picks)
        {
            totalPicks.Add(p);
        }

        foreach (GameObject i in item)
        {
            totalPicks.Add(i);
        }

        foreach (GameObject p in totalPicks)
        {
            if ((transform.position - p.transform.position).magnitude < pickupRadius && !currentlyHolding)
            {
                if (p.GetComponent<PickupScript>().pickupWeight > 1)
                {
                    p.GetComponent<PickupScript>().Hide();

                }
                else
                {
                    p.GetComponent<PickupScript>().Display();

                }
            }
            else
            {
                p.GetComponent<PickupScript>().Hide();
            }
        }

        // Press E to pick up and drop things
        if (Input.GetKeyDown(KeyCode.E) && !currentlyFixing)
        {
            if (!currentlyHolding && (playerMoveScript.grounded || playerMoveScript.onPlatform))
            {

                // Detects all pickups within range and essentially chooses one at random to pick up
                GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
                GameObject[] items = GameObject.FindGameObjectsWithTag("Item");

                List<GameObject> totalPickups = new(pickups.Length + items.Length);

                foreach (GameObject p in pickups)
                {
                    totalPickups.Add(p);
                }

                foreach (GameObject i in items)
                {
                    totalPickups.Add(i);
                }
                foreach (GameObject p in totalPickups)
                {
                    if ((transform.position - p.transform.position).magnitude < pickupRadius)
                    {
                        if (p.GetComponent<PickupScript>().pickupWeight <= 1)
                        {
                            objectHolding = p;

                        }
                        
                    }
                }
                objectHolding.GetComponent<PickupScript>().PickedUp(gameObject, playerModel);
                currentlyHolding = true;
            }
            else
            {
                objectHolding.GetComponent<PickupScript>().Dropped(gameObject);
                objectHolding = null;
                currentlyHolding = false;
            }
        }
    }

    void InteractFunctionality()
    {
        GameObject[] interacts = GameObject.FindGameObjectsWithTag("Interactable");
        foreach (GameObject i in interacts)
        {
            if (((transform.position - i.transform.position).magnitude < interactRadius && (!i.GetComponent<LeverTrigger>().oneTime || (i.GetComponent<LeverTrigger>().oneTime && !i.GetComponent<LeverTrigger>().oneTimeTriggered))) && !currentlyHolding)
            {
                i.GetComponent<LeverTrigger>().Display();
            }
            else
            {
                i.GetComponent<LeverTrigger>().Hide();
            }
        }

        // Press Q to interact with objects like levers
        if (Input.GetKeyDown(KeyCode.Q) && !currentlyFixing && !currentlyHolding)
        {
            GameObject objectInteracting = null;
            GameObject[] interactables = GameObject.FindGameObjectsWithTag("Interactable");
            foreach (GameObject i in interactables)
            {
                if ((i.transform.position - transform.position).magnitude <= interactRadius)
                {
                    objectInteracting = i;
                }
            }
            objectInteracting.GetComponent<LeverTrigger>().Interacted();
        }
    }

    void RobotFunctionality()
    {
        GameObject[] roboot = GameObject.FindGameObjectsWithTag("Robot");
        foreach (GameObject r in roboot)
        {
            if ((transform.position - r.transform.position).magnitude < interactRadius && !currentlyHolding && !currentlyFixing && !r.GetComponent<RobotScript>().gettingFixed && !r.GetComponent<RobotScript>().operative)
            {
                r.GetComponent<RobotScript>().Display();
            }
            else
            {
                r.GetComponent<RobotScript>().Hide();
            }
        }

        // Hold F to begin fixing robots
        if (Input.GetKeyDown(KeyCode.F) && !currentlyHolding)
        {
            if (!currentlyFixing)
            {
                GameObject[] robots = GameObject.FindGameObjectsWithTag("Robot");
                foreach (GameObject r in robots)
                {
                    if ((r.transform.position - transform.position).magnitude < pickupRadius)
                    {
                        currentRobot = r;
                        if (!currentRobot.GetComponent<RobotScript>().operative)
                        {
                            playerMoveScript.currentSpeed = 0;
                            playerMoveScript.jumpForce = 0;
                            r.GetComponent<RobotScript>().gettingFixed = true;
                            currentlyFixing = true;
                            break;
                        }
                    }
                }
                robotUI.SetActive(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.F) && !currentlyHolding && currentlyFixing)
        {
            playerMoveScript.currentSpeed = playerMoveScript.speedInit;
            playerMoveScript.jumpForce = playerMoveScript.jumpForceInit;
            currentRobot = null;
            currentlyFixing = false;
            robotUI.SetActive(false);
        }
    }
}