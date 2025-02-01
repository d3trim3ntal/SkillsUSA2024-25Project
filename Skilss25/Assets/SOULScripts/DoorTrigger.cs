using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public void trigger(GameObject door, int State)
    {
        if (State == 1)
        {
            door.GetComponent<DoorOpen>().open();
        }
        else if (State == -1)
        {
            door.GetComponent<DoorOpen>().close();
        }
    }
}
