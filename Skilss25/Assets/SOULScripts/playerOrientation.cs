using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerOrientation : MonoBehaviour
{
    // Camera turning speed
    public float rotationSpeed = 240;

    //Get these transforms in order to figure out where the camera is looking
    public Transform playerParent;
    public Transform orientation;
    public Transform playerObj;

    public Rigidbody rb;
    void Start()
    {
        
    }

    void Update()
    {
        //figure out where the camera is looking
        Vector3 viewDir = playerObj.position - new Vector3(transform.position.x, playerObj.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        float hori = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        //rotate player
        Vector3 inputDir = orientation.forward * vert + orientation.right * hori;
        if (inputDir != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }
}
