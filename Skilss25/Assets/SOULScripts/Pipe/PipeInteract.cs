using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeInteract : MonoBehaviour
{
    public GameObject PipeUI;
    public bool active;
    public CinemachineFreeLook cam;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (active)
            {
                Time.timeScale = 1;
                cam.enabled = true;
                active = false;
                Cursor.lockState = CursorLockMode.Locked;
                PipeUI.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                cam.enabled = false;
                active = true;
                Cursor.lockState = CursorLockMode.None;
                PipeUI.SetActive(true);
            }
        }
    }
}
