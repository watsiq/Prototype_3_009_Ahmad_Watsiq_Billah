using UnityEngine;

public class SpawnCars : MonoBehaviour
{
    public GameObject[] Cars;

    private void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int ram = Random.Range(0, Cars.Length);
            GameObject carInstance = Instantiate(Cars[ram], transform.GetChild(i).position, transform.GetChild(i).rotation);
            var carAI = carInstance.GetComponent<ICarAI>();
            carAI.currentTrafficRoute = this.gameObject;
            carAI.currentWaypointNumber = i;
        }
    }
}