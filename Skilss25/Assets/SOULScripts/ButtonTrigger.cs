using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    // MeshRenderer connection is just temporary
    public GameObject objectConnected;
    MeshRenderer mesh;
    public int state = -1;
    public float requiredWeight = 1;
    public float currentWeightOn = 0;
    public bool enoughWeight = false;
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
        if (currentWeightOn >= requiredWeight)
        {
            enoughWeight = true;
            if (GetComponent<PlatformTrigger>() != null)
            {
                PlatformTrigger platEvent = GetComponent<PlatformTrigger>();
                platEvent.DirectionSet(objectConnected, state);
            }
            else if (GetComponent<DoorTrigger>() != null)
            {
                GetComponent<DoorTrigger>().trigger(objectConnected, state);
            }
            state = 1;
            mesh.material.color = (Color.green);
        }
        else
        {
            enoughWeight = false;
            state = -1;
            mesh.material.color = (Color.red);
            if (GetComponent<PlatformTrigger>() != null)
            {
                PlatformTrigger platEvent = GetComponent<PlatformTrigger>();
                platEvent.DirectionSet(objectConnected, state);
            }
            else if (GetComponent<DoorTrigger>() != null)
            {
                GetComponent<DoorTrigger>().trigger(objectConnected, state);
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        // Checks what object is colliding, then executes similarly to lever
        if ((col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Pickup") || col.gameObject.CompareTag("Robot")))
        {
            if (enoughWeight)
            {
    
            }
            
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Pickup") || col.gameObject.CompareTag("Robot"))
        {
            //Weight Check
            objectsOn.Remove(col.gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Pickup") || col.gameObject.CompareTag("Robot"))
        {
            objectsOn.Add(col.gameObject);
            //WeightCheck
        }
    }

}
