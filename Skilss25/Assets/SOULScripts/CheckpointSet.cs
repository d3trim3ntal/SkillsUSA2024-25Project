using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSet : MonoBehaviour
{
    public bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerCheckpoint(Vector3 newCheckpoint)
    {
        if (!triggered)
        {
            PlayerCheckpointManager pcm = GameObject.FindWithTag("Player").GetComponent<PlayerCheckpointManager>();
            pcm.SetCheckpoint(newCheckpoint);
            triggered = true;
        }
    }
}
