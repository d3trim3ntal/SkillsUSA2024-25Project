using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject door;
    public bool openOrClosed = true;
    public bool active;
    
    public void Flip()
    {
        active = !active;
        openOrClosed = !openOrClosed;
        door.SetActive(openOrClosed);
    }
}
