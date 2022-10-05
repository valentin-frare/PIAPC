using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PatrollingState : IState
{
    private Queue<Transform> waypoints;
    private bool reached = true;
    private float playerDetectorRadius;
    private Transform agent;
    private Transform currentWaypoint;

    public Action<GameObject> OnPlayerEnterInRange;

    public PatrollingState(Queue<Transform> waypoints, Transform agent, float playerDetectorRadius)
    {
        this.waypoints = waypoints;
        this.playerDetectorRadius = playerDetectorRadius;
        this.agent = agent;
    }

    public void Update()
    {
        if (reached)
        {
            reached = false;
            var waypointToUse = waypoints.Dequeue();
            currentWaypoint = waypointToUse;
            waypoints.Enqueue(waypointToUse);
        }
        else
        {
            Debug.DrawLine(agent.position, currentWaypoint.position, Color.blue);
            agent.LookAt(currentWaypoint);
            agent.Translate(Vector3.forward * Time.deltaTime * 5f);
        }

        if (Vector3.Distance(agent.position, currentWaypoint.position) < 1)
        {
            reached = true;
        }

        Collider[] hitColliders = Physics.OverlapSphere(agent.transform.position, playerDetectorRadius);
        if (hitColliders.Length > 0)
        {
            Collider player = hitColliders.Where( collider => collider.gameObject.tag == "Player" ).FirstOrDefault();

            if (player != null)
            {
                OnPlayerEnterInRange?.Invoke(player.gameObject);
            }
        }
    }
}