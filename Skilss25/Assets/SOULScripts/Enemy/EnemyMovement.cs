using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Android;
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    public bool movable = true;

    //knockback timer
    private float kbStart;
    private float kbDuration;
    private bool inkb = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //movement
        if (movable)
        {
            agent.SetDestination(player.transform.position);
        }
        //kb timer
        if (inkb)
        {
            float kbLeft = Time.time - kbStart;
            if (kbStart > kbDuration)
            {
                inkb = false;
                movable = true;
                agent.isStopped = false;
            }
        }

        //knockback testing
        if (Input.GetMouseButtonDown(0))
        {
            hitstun(2f);
        }
    }

    public void hitstun(float duration)
    {
        movable = false;
        inkb = true;
        agent.SetDestination(transform.position);
        kbStart = Time.time;
        kbDuration = duration;
    }


}
