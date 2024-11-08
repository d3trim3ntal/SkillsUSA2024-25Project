using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpointManager : MonoBehaviour
{
    public Vector3 checkpoint = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        checkpoint = newCheckpoint;
    }

    public void Respawn()
    {
        transform.position = checkpoint + Vector3.up;
    }
}
