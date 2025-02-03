using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTrigger : MonoBehaviour
{
    // MeshRenderer connection is just temporary to show what state it's in
    public List<GameObject> connectedObjects;
    public bool oneTime = false;
    public bool oneTimeTriggered = false;
    public int currentState = -1;
    public GameObject displayText;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interacted()
    {
        if (!oneTimeTriggered)
        {
            currentState *= -1;

            // The next couple of if statements will check for certain scripts on each lever; if they have it, they will execute that script
            foreach (GameObject connected in connectedObjects)
            {
                if (GetComponent<PlatformTrigger>() != null && connected.GetComponent<PlatformMovement>() != null)
                {
                    GetComponent<PlatformTrigger>().DirectionSet(connected, currentState);
                }

                if (GetComponent<CheckpointSet>() != null)
                {
                    GetComponent<CheckpointSet>().SetPlayerCheckpoint(transform.position);
                }
            }
            if (oneTime)
            {
                oneTimeTriggered = true;
            }
        }
    }

    public void Display()
    {
        displayText.SetActive(true);
    }

    public void Hide()
    {
        displayText.SetActive(false);
    }
}
