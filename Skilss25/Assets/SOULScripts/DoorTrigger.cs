using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public void trigger(GameObject door, int State)
    {
        door.GetComponent<DoorOpen>().Flip();
    }
}
