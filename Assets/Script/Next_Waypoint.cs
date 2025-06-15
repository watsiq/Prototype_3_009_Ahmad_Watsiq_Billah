using UnityEngine;

public class Next_Waypoint : MonoBehaviour
{
    public GameObject[] Waypoints;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            var carAI = other.GetComponent<ICarAI>();
            carAI.nextWaypoint = Waypoints.Length == 0 ? null : Waypoints[Random.Range(0, Waypoints.Length)];
        }
    }
}