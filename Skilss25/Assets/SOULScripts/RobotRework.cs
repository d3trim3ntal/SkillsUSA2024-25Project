using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotRework : MonoBehaviour
{
    Marker[] markers;
    private NavMeshAgent agent;

    List<int> assignedActions = new List<int>();
    //1 for movement
    private int currentAction;
    private int runningAction;
    public int maxActions;
    public float markerRange = 10;

    private Dictionary<int, GameObject> assignedMarkers = new Dictionary<int, GameObject>();
    //State bools
    public bool running;
    public bool beingFixed;
    public bool moving;
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
                    beingFixed = true;
                }
            }

            if (beingFixed && !selecting)
            {
                //Temporary key for starting location selection
                if (Input.GetKeyDown(KeyCode.M))
                {
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
        }
        
    }


    public void assignMarker(GameObject marker)
    {
        Cursor.lockState = CursorLockMode.Locked;

        selecting = false;
        foreach (Marker i in markers)
        {
            i.deactivate();
        }
        assignedMarkers[currentAction] = marker;
        assignedActions.Add(1);
        currentAction++;
        if (currentAction == maxActions--)
        {
            initiateActions();
        }


    }

    private void initiateActions()
    {
        beingFixed = false;
        running = true;
        if (currentAction > 0)
        {
            if (assignedActions[runningAction] == 1)
            {
                moving = true;
                agent.SetDestination(assignedMarkers[runningAction].transform.position);
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
        running = false;
    }
}
