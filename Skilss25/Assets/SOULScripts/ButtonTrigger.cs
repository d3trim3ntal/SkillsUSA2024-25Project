using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    // MeshRenderer connection is just temporary
    public GameObject objectConnected;
    MeshRenderer mesh;
    public int state = -1;
    public float requiredWeight = 1;
    float currentWeightOn = 0;
    public List<GameObject> objectsOn;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        currentWeightOn = 0;
        foreach (GameObject g in objectsOn)
        {
            currentWeightOn += g.GetComponent<ButtonWeight>().weight;
        }
    }

    void OnTriggerStay(Collider col)
    {
        // Checks what object is colliding, then executes similarly to lever
        if ((col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Pickup") || col.gameObject.CompareTag("Robot")) && currentWeightOn >= requiredWeight)
        {
            if (GetComponent<PlatformTrigger>() != null)
            {
                PlatformTrigger platEvent = GetComponent<PlatformTrigger>();
                platEvent.DirectionSet(objectConnected, state);
            }
            state = 1;
            mesh.material.color = (Color.green);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Pickup") || col.gameObject.CompareTag("Robot"))
        {
            if (currentWeightOn <= requiredWeight)
            {
                state = -1;
                mesh.material.color = (Color.red);
                if (GetComponent<PlatformTrigger>() != null)
                {
                    PlatformTrigger platEvent = GetComponent<PlatformTrigger>();
                    platEvent.DirectionSet(objectConnected, state);
                }
            }
            objectsOn.Remove(col.gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Pickup") || col.gameObject.CompareTag("Robot"))
        {
            objectsOn.Add(col.gameObject);
        }
    }

}
