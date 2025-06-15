using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Car_AI_Selected : MonoBehaviour
{
    public float safeDistance = 2f;
    public float carSpeed = 5f;
    private bool canMove = true;
    public string[] tags;

    public GameObject currentTrafficRoute;
    public GameObject nextWaypoint;
    public int currentWapointNumber;

    private NavMeshAgent _carNavmesh;

    public bool isSelected = false;
    public bool overrideTrafficControl = false;

    private void Start()
    {
        _carNavmesh = GetComponent<NavMeshAgent>();
        _carNavmesh.speed = carSpeed;
    }

    public void Select()
    {
        isSelected = true;
        overrideTrafficControl = true; // Abaikan sistem lampu saat user pegang
    }

    public void Deselect()
    {
        isSelected = false;
        overrideTrafficControl = false; // Kembali ke sistem otomatis jika diperlukan
    }

    private void Update()
    {
        if (!canMove) return;

        // Cegah tabrakan jika tidak override
        if (!overrideTrafficControl)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, safeDistance))
            {
                foreach (string t in tags)
                {
                    if (hit.transform.CompareTag(t))
                    {
                        Stop();
                        return;
                    }
                }
            }
        }

        Move();
    }

    void Stop()
    {
        _carNavmesh.speed = 0;
    }

    void Move()
    {
        if (_carNavmesh.speed == 0)
            _carNavmesh.speed = carSpeed;

        if (nextWaypoint == null && currentWapointNumber <= 0)
        {
            _carNavmesh.speed = 0;
            return;
        }

        // Gerak ke waypoint
        if (currentWapointNumber > 0)
        {
            Vector3 target = currentTrafficRoute.transform.GetChild(currentWapointNumber - 1).position;
            _carNavmesh.SetDestination(target);

            if (Vector3.Distance(transform.position, target) <= 1f)
                currentWapointNumber--;
        }
        else if (nextWaypoint != null)
        {
            _carNavmesh.SetDestination(nextWaypoint.transform.position);

            if (Vector3.Distance(transform.position, nextWaypoint.transform.position) <= 1f)
            {
                currentWapointNumber = 4; // Reset ke awal
                currentTrafficRoute = nextWaypoint.transform.parent.gameObject;
            }
        }
    }

    public void Go()
    {
        if (isSelected)
        {
            canMove = true;
            _carNavmesh.speed = carSpeed;
        }
    }

    public void StopByHand()
    {
        if (isSelected)
        {
            canMove = false;
            _carNavmesh.speed = 0;
        }
    }
}
