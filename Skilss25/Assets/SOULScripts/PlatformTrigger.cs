using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Sets direction for the platform to go depending on the state of the lever
    public void DirectionSet(GameObject platform, int state)
    {
        if (state == 1)
        {
            platform.GetComponent<PlatformMovement>().GoForward();
        }
        else if (state == -1)
        {
            platform.GetComponent<PlatformMovement>().GoBackward();
        }
    }
}
