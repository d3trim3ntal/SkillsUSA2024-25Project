using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTriggerPoint : MonoBehaviour
{
    // MeshRenderer connection is just temporary to show what state it's in
    public GameObject connectedObject;
    MeshRenderer mesh;
    public int currentState = -1;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interacted()
    {
        Color coloring = mesh.material.color;
        mesh.material.color = new Color(coloring.r + currentState * -1, coloring.g + currentState * -1, coloring.b + currentState * -1);
        currentState *= -1;

        // The next couple of if statements will check for certain scripts on each lever; if they have it, they will execute that script
        if (GetComponent<PlatformTrigger>() != null)
        {
            GetComponent<PlatformTrigger>().DirectionSet(connectedObject, currentState);
        }

        if (GetComponent<CheckpointSet>() != null)
        {
            GetComponent<CheckpointSet>().SetPlayerCheckpoint(transform.position);
        }

        if (GetComponent<PipeManager>() != null)
        {
            GetComponent<PipeManager>().toggleCam();
        }
    }
}
