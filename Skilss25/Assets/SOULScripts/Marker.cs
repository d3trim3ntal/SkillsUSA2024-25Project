using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    public GameObject button;
    private RobotRework robot;
    private bool showing;
    public void Update()
    {
        if (showing && Input.GetKeyDown(KeyCode.O))
        {
            choose();
        }
    }
    public void activate(RobotRework caller)
    {
        showing = true;
        robot = caller;
        button.SetActive(true);
    }

    public void deactivate()
    {
        showing = false;
        robot = null;
        button.SetActive(false);
    }

    public void choose()
    {
        robot.assignMarker(gameObject);
    }
}
