using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ButtonWeight : MonoBehaviour
{
    public float weight = 1;
    float weightInit = 0;
    public List<GameObject> collidingWith;
    // Start is called before the first frame update
    void Start()
    {
        weightInit = weight;
    }

    // Update is called once per frame
    void Update()
    {
        weight = weightInit;
        foreach (GameObject c in collidingWith)
        {
            weight += c.GetComponent<ButtonWeight>().weight;
        }
    }

    public void OnCollisionStay(Collision c)
    {
        if (c.gameObject.transform.position.y >= transform.position.y + 0.45f && c.gameObject.GetComponent<ButtonWeight>() != null && !collidingWith.Contains(c.gameObject))
        {
            collidingWith.Add(c.gameObject);
        }
    }

    public void OnCollisionExit(Collision c)
    {
        if (collidingWith.Contains(c.gameObject))
        {
            collidingWith.Remove(c.gameObject);
        }
    }
}
