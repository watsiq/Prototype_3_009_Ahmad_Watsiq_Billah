using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Car_AI : MonoBehaviour, ICarAI
{
    public float safeDistance = 2f;
    public float carSpeed = 5f;
    public string[] tags;

    public GameObject currentTrafficRoute { get; set; }
    public GameObject nextWaypoint { get; set; }
    public int currentWaypointNumber { get; set; }

    protected NavMeshAgent carNavmesh;

    protected virtual void Start()
    {
        carNavmesh = GetComponent<NavMeshAgent>();
        carNavmesh.speed = carSpeed;
    }

    protected virtual void Update()
    {
        if (DetectObstacle())
            Stop();
        else
            Move();
    }

    protected bool DetectObstacle()
    {
        return Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, safeDistance) &&
               System.Array.Exists(tags, tag => hit.transform.CompareTag(tag));
    }

    protected void Stop()
    {
        carNavmesh.speed = 0;
    }

    protected virtual void Move()
    {
        if (nextWaypoint == null && currentWaypointNumber == 0)
        {
            Stop();
            return;
        }

        if (currentWaypointNumber > 0)
        {
            if (carNavmesh.speed == 0) carNavmesh.speed = carSpeed;
            carNavmesh.SetDestination(currentTrafficRoute.transform.GetChild(currentWaypointNumber - 1).position);

            if (Vector3.Distance(transform.position, currentTrafficRoute.transform.GetChild(currentWaypointNumber - 1).position) <= 1)
                currentWaypointNumber--;
        }
        else if (nextWaypoint != null)
        {
            if (carNavmesh.speed == 0) carNavmesh.speed = carSpeed;
            carNavmesh.SetDestination(nextWaypoint.transform.position);

            if (Vector3.Distance(transform.position, nextWaypoint.transform.position) <= 1)
            {
                currentWaypointNumber = 4;
                currentTrafficRoute = nextWaypoint.transform.parent.gameObject;
            }
        }
    }
}