using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;


public class Enemycontroller : MonoBehaviour
{
    float speed = 1f;
    //Rigidbody rb;
    NavMeshAgent agent;
    Animator anim;
    [SerializeField]
    List<Transform> waypoints = new List<Transform>();
    [SerializeField]
    float waitTimeAtPoint = 3f;
    [SerializeField]
    bool patrolInloop = true;
    int currentWaypointIndex = 0;
    bool iswaiting = false;
    bool movingForward = true;


    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        if (waypoints == null || waypoints.Count == 0) return;

        GoToCurrentWaypoint();
    }

    void GoToCurrentWaypoint()
    {
        if (waypoints.Count == 0) return;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    void SelectNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
    }

    IEnumerator WaitAtWaypoint()
    {
        iswaiting = true;
        yield return new WaitForSeconds(waitTimeAtPoint);
        SelectNextWaypoint();
        anim.SetTrigger("Walk");
        agent.speed = speed;
        GoToCurrentWaypoint();
        iswaiting = false;
    }

    void Update()
    {
        if (waypoints.Count == 0 || iswaiting) return;
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            anim.SetTrigger("Stop");
            agent.speed = 0;
            StartCoroutine(WaitAtWaypoint());
        }

        //Vector3 forwardMove = transform.forward * speed;
        //rb.linearVelocity = new Vector3(forwardMove.x, rb.linearVelocity.y, forwardMove.z);
    }
}
