using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    // Whether or not it's picked up and object holding it
    public bool pickedUp;
    GameObject objPicking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUp)
        {
            transform.position = objPicking.transform.position + objPicking.transform.forward;
            transform.Rotate(Vector3.up * 90 * Time.deltaTime);
        }
    }

    public void PickedUp(GameObject gamer)
    {
        // Sets the "parent" as the gameobject picking this item up
        objPicking = gamer;
        pickedUp = true;
        GetComponent<BoxCollider>().isTrigger = true;
    }

    public void Dropped(GameObject gamer)
    {
        objPicking = null;
        pickedUp = false;
        GetComponent<BoxCollider>().isTrigger = false;
    }

    void OnCollisionEnter(Collision c)
    {
        // Same as PlayerMovement
        if (c.gameObject.CompareTag("Platform") || c.gameObject.CompareTag("Robot"))
        {
            transform.parent = c.gameObject.transform;
        }
    }

    void OnCollisionExit(Collision c)
    {
        if (c.gameObject.CompareTag("Platform") || c.gameObject.CompareTag("Robot"))
        {
            transform.parent = null;
        }
    }

}
