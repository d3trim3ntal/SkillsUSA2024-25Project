using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    // How far the platform can be from the destination and still progress
    public float distanceLeniency;
    // Set nodes and how long it takes to travel between them
    public Vector3 startNode;
    public Vector3 endNode;
    public Vector3[] nodeList;
    public float timeBetweenNodes;
    // What direction the platform will cycle between nodes and which node it is currently on
    public int nodeDirection = 0;
    public int currentNode = 0;
    // Start is called before the first frame update
    void Start()
    {
        // Set start and end nodes, make platform go to first node
        startNode = nodeList[0];
        endNode = nodeList[nodeList.Length - 1];
        transform.position = startNode;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks to make sure platform doesn't get stuck on start or end node, translates it from one node to the next
        if ((nodeDirection == 1 && currentNode != 0) || (nodeDirection == -1 && currentNode != nodeList.Length - 1))
        {
            transform.Translate((nodeList[currentNode] - nodeList[currentNode - nodeDirection]) / timeBetweenNodes * Time.deltaTime);
        }
        // If the platform is close enough to destination
        if ((transform.position - nodeList[currentNode]).magnitude <= distanceLeniency)
        {
            // Cycle to next node
            currentNode += nodeDirection;
            if (currentNode < 0)
            {
                // If already at start node, stay at start node
                currentNode = 0;
                nodeDirection = 0;
            }
            else if (currentNode > nodeList.Length - 1)
            {
                // If already at end node, stay at end node
                currentNode = nodeList.Length - 1;
                nodeDirection = 0;
            }
        }
    }

    // Sets platform to cycle through nodes going up
    public void GoForward()
    {
        if (nodeList[currentNode + 1] != null)
        {
            if (nodeDirection == -1)
            {
                currentNode++;
            }
            nodeDirection = 1;
        }
    }
    // Sets platform to cycle through nodes in opposite direction
    public void GoBackward()
    {
        if (nodeList[currentNode - 1] != null)
        {
            if (nodeDirection == 1)
            {
                currentNode--;
            }
            nodeDirection = -1;
        }
    }
}