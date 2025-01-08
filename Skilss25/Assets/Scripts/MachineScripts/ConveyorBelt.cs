using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public List<GameObject> onBelt;
    public List <GameObject> tools;
    public GameObject machineCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i <= onBelt.Count - 1; i++)
        {
            onBelt[i].GetComponent<Rigidbody>().velocity = speed * direction;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
        Debug.Log("On belt");
    }

    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
        collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Debug.Log("Off belt");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            ReplaceWithRandomTool(other.gameObject);
        }
    }

    public void ReplaceWithRandomTool(GameObject item)
    {
        int randomIndex = Random.Range(0, tools.Count);
        GameObject randomTool = tools[randomIndex];
        GameObject newTool = Instantiate(randomTool, item.transform.position, item.transform.rotation);
        onBelt.Remove(item);
        Destroy(item);
    }

}
