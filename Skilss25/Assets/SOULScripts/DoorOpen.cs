using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject door;
    public void open()
    {
        door.SetActive(false);
    }

    public void close()
    {
        door.SetActive(true);
    }
}
