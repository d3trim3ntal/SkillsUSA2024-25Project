using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRescale : MonoBehaviour
{
    public GameObject parentObject;
    public float[] defaults = new float[3] { 6.239f, 3.8796f, 4.3134f };
    public float[] gearDefaults = new float[3] { 25.6483f, 25.6483f, 25.6483f * 2 };

    // Start is called before the first frame update
    void Start()
    {
        parentObject = transform.parent.gameObject.transform.parent.gameObject;
        transform.localScale = new Vector3(transform.localScale.x / (parentObject.transform.localScale.x), transform.localScale.y / (parentObject.transform.localScale.y), transform.localScale.z / (parentObject.transform.localScale.z));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
