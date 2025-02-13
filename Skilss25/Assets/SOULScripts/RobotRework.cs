using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class RobotRework : MonoBehaviour
{
    Marker[] markers;
    private NavMeshAgent agent;

    List<int> assignedActions = new List<int>();
    //currentaction is used when assigning actions, while runningaction is used when the robot is performing the actions
    private int currentAction;
    private int runningAction;
    public int maxActions;
    public float markerRange = 10;

    private GameObject closest;
    private Dictionary<int, GameObject> assignedMarkers = new Dictionary<int, GameObject>();

    [Header("UI")]
    public TextMeshProUGUI fixText;
    public TextMeshProUGUI selectText;
    public TextMeshProUGUI sequenceText;
    public TextMeshProUGUI actionsText;

    [Header("State Bools")]
    public bool running;
    public bool beingFixed;
    public bool moving;
    public bool pickingUp;
    private bool selecting; //Selecting location
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        markers = FindObjectsOfType<Marker>();
    }

    void Update()
    {


        if (!running)
        {
            //Cancel marker select
            if (selecting && Input.GetMouseButtonDown(1))
            {
                fixText.gameObject.SetActive(true);
                selectText.gameObject.SetActive(false);

                selecting = false;
                foreach (Marker i in markers)
                {
                    i.deactivate();
                }
                Cursor.lockState = CursorLockMode.Locked;

            }

            //Initiate fix
            if (!selecting && Input.GetKeyDown(KeyCode.L))
            {
                if (beingFixed)
                {
                    initiateActions();
                }
                else
                {
                    updateSequenceText();
                    beingFixed = true;
                    fixText.gameObject.SetActive(true);
                }
            }

            if (beingFixed && !selecting && assignedActions.Count != maxActions)
                
            {
                //Temporary key for starting location selection
                if (Input.GetKeyDown(KeyCode.M))
                {
                    fixText.gameObject.SetActive(false);
                    selectText.gameObject.SetActive(true);

                    selecting = true;
                    foreach (Marker i in markers)
                    {
                        if (Vector3.Distance(transform.position, i.transform.position) < markerRange)
                        {
                            i.activate(this);
                        }
                    }
                    Cursor.lockState = CursorLockMode.None;
                }
                else if (Input.GetKeyDown(KeyCode.Semicolon))
                {
                    assignedActions.Add(2);
                    currentAction++;
                    updateSequenceText();
                }
            }
        }
        else
        {
            //Checks while running
            if (moving && Vector3.Distance(assignedMarkers[runningAction].transform.position, transform.position) < 1f)
            {
                agent.SetDestination(transform.position);
                moving = false;
                continueActions();

                
            }
            if (pickingUp && Vector3.Distance(closest.transform.position, transform.position) < 3f)
            {
                closest = null;
                pickingUp = false;
                continueActions();
            }
        }
        
    }


    public void assignMarker(GameObject marker)
    {
        selectText.gameObject.SetActive(false);
        fixText.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;

        selecting = false;
        foreach (Marker i in markers)
        {
            i.deactivate();
        }
        assignedMarkers[currentAction] = marker;
        assignedActions.Add(1);
        currentAction++;
        updateSequenceText();

        


    }

    private void initiateActions()
    {
        fixText.gameObject.SetActive(false);
        beingFixed = false;
        running = true;
        if (currentAction > 0)
        {
            if (assignedActions[runningAction] == 1)
            {
                moving = true;
                agent.SetDestination(assignedMarkers[runningAction].transform.position);
            }
            else if (assignedActions[runningAction] == 2)
            {
                GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
                float closestDistance = 25f;
                closest = null;
                foreach (GameObject pickup in pickups)
                {
                    if (Vector3.Distance(transform.position, pickup.transform.position) < closestDistance)
                    {
                        closest = pickup;
                        closestDistance = Vector3.Distance(transform.position, pickup.transform.position);
                    }
                }

                if (closest != null)
                {
                    pickingUp = true;
                    agent.SetDestination(closest.transform.position);
                }
                else
                {
                    continueActions();
                }

            }
        }
        else
        {
            endActions();
        }
    }

    private void continueActions()
    {
        runningAction++;
        if (runningAction < currentAction)
        {
            if (currentAction > 0)
            {
                if (assignedActions[runningAction] == 1)
                {
                    moving = true;
                    agent.SetDestination(assignedMarkers[runningAction].transform.position);
                }
                //Pickup
                else if (assignedActions[runningAction] == 2)
                {
                    GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
                    float closestDistance = 25f;
                    closest = null;
                    foreach (GameObject pickup in pickups)
                    {
                        if (Vector3.Distance(transform.position, pickup.transform.position) < closestDistance)
                        {
                            closest = pickup;
                            closestDistance = Vector3.Distance(transform.position, pickup.transform.position);
                        }
                    }

                    if (closest != null)
                    {
                        pickingUp = true;
                        agent.SetDestination(closest.transform.position);
                    }
                    else
                    {
                        continueActions();
                    }

                }
            }
        }
        else
        {
            endActions();
        }
        
    }

    private void endActions()
    {
        assignedActions.Clear();
        runningAction = 0;
        currentAction = 0;
        updateSequenceText();
        running = false;

    }

    private void updateSequenceText()
    {
        string guy = "";

        foreach (int i in assignedActions)
        {
            if (i == 1)
            {
                guy += "Move ";
            }
            if (i == 2)
            {
                guy += "Pickup ";
            }
        }
        sequenceText.text = guy;

        actionsText.text = (maxActions - assignedActions.Count) + " actions left.";
    }

    
}
