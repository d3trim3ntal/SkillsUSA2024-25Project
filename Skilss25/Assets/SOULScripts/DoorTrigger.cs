using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    DoorOpen open;
    public void trigger(GameObject door, int State)
    {
        //Assign open
        if (open == null)
        {
            open = door.GetComponent<DoorOpen>();
        }

        //Make bool out of state int
        bool stateBool;
        if (State == 1)
        {
            stateBool = true;
        }
        else
        {
            stateBool = false;
        }

        if (open.openOrClosed == stateBool)
        {
            open.Flip();
        }
    }

    public void flip(GameObject door)
    {
        door.GetComponent<DoorOpen>().Flip();
    }
}
