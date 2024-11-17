using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    Rigidbody rb;
    // Whether or not it's picked up and object holding it
    public bool pickedUp;
    GameObject objPicking;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

    public void PickedUp(GameObject gamer, GameObject model)
    {
        // Checks if the object was snatched
        if (gamer != objPicking && objPicking != null)
        {
            if (objPicking.GetComponent<RobotScript>() != null)
            {
                objPicking.GetComponent<RobotScript>().currentlyHolding = false;
                objPicking.GetComponent<RobotScript>().objectHolding = null;
            }
            else if (objPicking.GetComponent<PlayerAbilities>() != null)
            {
                objPicking.GetComponent<PlayerAbilities>().currentlyHolding = false;
                objPicking.GetComponent<PlayerAbilities>().objectHolding = null;
            }
        }
        transform.parent = null;
        transform.localScale = Vector3.one;
        // Sets the "parent" as the model of gameobject picking this item up
        objPicking = model;
        pickedUp = true;
        GetComponent<BoxCollider>().isTrigger = true;
        // Prevent downward acceleration
        rb.useGravity = false;

    }

    public void Dropped(GameObject gamer)
    {
        GetComponent<BoxCollider>().isTrigger = false;
        objPicking = null;
        pickedUp = false;
        rb.useGravity = true;
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
