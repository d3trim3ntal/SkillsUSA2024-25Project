using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    private RobotRework robot;
    private bool showing;
    
    public void activate(RobotRework caller)
    {
        showing = true;
        robot = caller;
    }

    public void deactivate()
    {
        showing = false;
        robot = null;
    }

    public void choose()
    {
        robot.assignMarker(gameObject);
    }
    private void OnMouseDown()
    {
        if (showing)
        {
            choose();
        }
    }
}


