using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    Rigidbody rb;
    ParticleSystem smoke;
    public bool gettingFixed = false;
    public bool operative = false;
    public char[] availableSequence;
    public List<char> currentSequence = new List<char>();
    public int currentPartRunning;
    public float locateRad;
    public float pickupRad = 2.5f;
    float currentClosest = 0;
    public float moveSpeed;
    public bool onTheMove;
    public bool currentlyHolding;
    public GameObject objectHolding;
    GameObject objectToGoTo = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        smoke = GetComponent<ParticleSystem>();
        currentClosest = locateRad;
    }

    // Update is called once per frame
    void Update()
    {
        if (gettingFixed)
        {
            // If the player inputs the numbers 1-5, add it to a sequence
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                foreach (char c in availableSequence)
                {
                    if (c.ToString() == "1")
                    {
                        currentSequence.Add(c);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                foreach (char c in availableSequence)
                {
                    if (c.ToString() == "3")
                    {
                        currentSequence.Add(c);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                foreach (char c in availableSequence)
                {
                    if (c.ToString() == "4")
                    {
                        currentSequence.Add(c);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                foreach (char c in availableSequence)
                {
                    if (c.ToString() == "5")
                    {
                        currentSequence.Add(c);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                foreach (char c in availableSequence)
                {
                    if (c.ToString() == "2")
                    {
                        currentSequence.Add(c);
                    }
                }
            }

            // Releasing F starts the code
            if (Input.GetKeyUp(KeyCode.F))
            {
                currentPartRunning = 0;
                ReadyToGo();
            }
        }

        // Makes the robot look at their target for movement
        if (objectToGoTo != null)
        {
            transform.LookAt(objectToGoTo.transform.position - Vector3.up * (objectToGoTo.transform.position.y - transform.position.y));
        }
    }

    void ReadyToGo()
    {
        StartCoroutine(CodeRun());
        gettingFixed = false;
        operative = true;
    }

    IEnumerator CodeRun()
    {
        yield return new WaitForSeconds(0.5f);
        if (currentPartRunning < currentSequence.Count)
        {
            // Checks which number is next and executes it
            if (char.ToString(currentSequence[currentPartRunning]) == "1")
            {
                StartCoroutine(Locate());
            }
            if (char.ToString(currentSequence[currentPartRunning]) == "3")
            {
                onTheMove = true;
                StartCoroutine(GoToObject());
            }
            if (char.ToString(currentSequence[currentPartRunning]) == "4")
            {
                StartCoroutine(PickUp());
            }
            if (char.ToString(currentSequence[currentPartRunning]) == "2")
            {
                StartCoroutine(Return());
            }
            if (char.ToString(currentSequence[currentPartRunning]) == "5")
            {
                StartCoroutine(Drop());
            }
            currentPartRunning++;
        }
        else
        {
            currentSequence.Clear();
            objectToGoTo = null;
            operative = false;
            smoke.Play();
        }
    }

    IEnumerator Locate()
    {
        objectToGoTo = null;
        yield return new WaitForSeconds(0.75f);
        // Locates all pickups in the world, then chooses one to go to within range
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
        currentClosest = locateRad;
        foreach (GameObject p in pickups)
        {
            if ((p.transform.position - transform.position).magnitude < currentClosest)
            {
                currentClosest = (p.transform.position - transform.position).magnitude;
                objectToGoTo = p;
            }
        }
        StartCoroutine(CodeRun());
    }

    IEnumerator GoToObject()
    {
        if (onTheMove && objectToGoTo != null)
        {
            // Ensures horizontal velocity is controlled
            rb.velocity = Vector3.zero + Vector3.up * rb.velocity.y;
            // Stops robot if close enough to target
            if ((objectToGoTo.transform.position - transform.position).magnitude < pickupRad)
            {
                onTheMove = false;
                StartCoroutine(CodeRun());
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                rb.AddRelativeForce(Vector3.forward * moveSpeed, ForceMode.VelocityChange);
                yield return new WaitForEndOfFrame();
                StartCoroutine(GoToObject());
            }
        }
    }

    IEnumerator PickUp()
    {
        yield return new WaitForSeconds(0.4f);
        if (objectToGoTo.CompareTag("Pickup"))
        {
            objectToGoTo.GetComponent<PickupScript>().PickedUp(gameObject);
            objectHolding = objectToGoTo;
            currentlyHolding = true;
        }
        StartCoroutine(CodeRun());
    }

    IEnumerator Drop()
    {
        yield return new WaitForSeconds(0.4f);
        if (currentlyHolding)
        {
            objectHolding.GetComponent<PickupScript>().Dropped(gameObject);
            objectHolding = null;
            currentlyHolding = false;
        }
        StartCoroutine(CodeRun());
    }

    IEnumerator Return()
    {
        yield return new WaitForSeconds(0.75f);
        objectToGoTo = GameObject.FindWithTag("Player");
        StartCoroutine(CodeRun());
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Platform"))
        {
            transform.parent = c.gameObject.transform;
        }
    }

    void OnCollisionExit(Collision c)
    {
        if (c.gameObject.CompareTag("Platform"))
        {
            transform.parent = null;
        }
    }
}
