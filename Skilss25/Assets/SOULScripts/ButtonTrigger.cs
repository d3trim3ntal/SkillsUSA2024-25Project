using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    // MeshRenderer connection is just temporary
    public GameObject objectConnected;
    MeshRenderer mesh;
    public int state = -1;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider col)
    {
        // Checks what object is colliding, then executes similarly to lever
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Pickup") || col.gameObject.CompareTag("Robot"))
        {
            state = 1;
            mesh.material.color = (Color.green);
            if (GetComponent<PlatformTrigger>() != null)
            {
                PlatformTrigger platEvent = GetComponent<PlatformTrigger>();
                platEvent.DirectionSet(objectConnected, state);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Pickup") || col.gameObject.CompareTag("Robot"))
        {
            state = -1;
            mesh.material.color = (Color.red);
            if (GetComponent<PlatformTrigger>() != null)
            {
                PlatformTrigger platEvent = GetComponent<PlatformTrigger>();
                platEvent.DirectionSet(objectConnected, state);
            }
        }
    }

}
