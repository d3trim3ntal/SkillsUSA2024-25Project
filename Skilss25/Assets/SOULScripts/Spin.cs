using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public bool toggle = true;
    public float rotationSpeedX = 0.0f;
    public float rotationSpeedY = 0.0f;
    public float rotationSpeedZ = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (toggle)
        {
            //Get the current rotationSpeed
            Vector3 currentRotation = transform.rotation.eulerAngles;

            //Update the rotation based on the specified speeds
            float newRotationX = currentRotation.x + rotationSpeedX * Time.deltaTime;
            float newRotationY = currentRotation.y + rotationSpeedY * Time.deltaTime;
            float newRotationZ = currentRotation.z + rotationSpeedZ * Time.deltaTime;

            //Apply the new rotation
            transform.rotation = Quaternion.Euler(newRotationX, newRotationY, newRotationZ);
        }
    }
}
