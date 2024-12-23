using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            c.gameObject.GetComponent<PlayerCheckpointManager>().Respawn();
        }
        else if (c.gameObject.CompareTag("Pickup"))
        {
            c.gameObject.GetComponent<PickupRespawn>().Respawn();
        }
    }
}
