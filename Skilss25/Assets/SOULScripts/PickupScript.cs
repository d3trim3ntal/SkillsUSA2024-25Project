using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    Rigidbody rb;
    // Whether or not it's picked up and object holding it
    public bool pickedUp;
    GameObject objPicking;
    public GameObject displayText;

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
            if (objPicking.GetComponent<PlayerAbilities>() != null)
            {
                transform.position = objPicking.transform.position - objPicking.transform.right * 1.5f + 2.5f * Vector3.up;
            }
            else
            {
                transform.position = objPicking.transform.position + objPicking.transform.up * 0.5f + objPicking.transform.forward * 0.75f;
            }
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
        if ((GetComponent<ButtonWeight>().weight > 1 && gamer.GetComponent<RobotScript>() !=null) || (GetComponent<ButtonWeight>().weight <= 1))
        {
            transform.parent = null;
            transform.localScale = Vector3.one;
            // Sets the "parent" as the model of gameobject picking this item up
            objPicking = model;
            pickedUp = true;
            GetComponent<BoxCollider>().isTrigger = true;
            // Prevent downward acceleration
            rb.useGravity = false;
        }

    }

    public void Dropped(GameObject gamer)
    {
        GetComponent<BoxCollider>().isTrigger = false;
        objPicking = null;
        pickedUp = false;
        rb.useGravity = true;
        rb.velocity = Vector3.zero;
        rb.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void OnCollisionEnter(Collision c)
    {
        // Same as PlayerMovement
        if (c.gameObject.CompareTag("Platforms") || c.gameObject.CompareTag("Robot"))
        {
            transform.parent = c.gameObject.transform;
        }
    }

    void OnCollisionExit(Collision c)
    {
        if (c.gameObject.CompareTag("Platforms") || c.gameObject.CompareTag("Robot"))
        {
            transform.parent = null;
            rb.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void Display()
    {
        displayText.SetActive(true);
    }

    public void Hide()
    {
        displayText.SetActive(false);
    }
}
