using UnityEngine;

public interface ICarAI
{
    GameObject currentTrafficRoute { get; set; }
    GameObject nextWaypoint { get; set; }
    int currentWaypointNumber { get; set; }
}