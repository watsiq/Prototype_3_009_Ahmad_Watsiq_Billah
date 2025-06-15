using UnityEngine;

public class TrafficController : MonoBehaviour
{
    [Header("Raycast Settings")]
    public Transform rayOrigin; // Bisa diassign dari Inspector (misalnya finger tip)
    public LayerMask vehicleLayer;
    public float rayLength = 20f;

    private GameObject selectedVehicle;

    public void SendGoCommand()
    {
        if (selectedVehicle != null)
        {
            var ai = selectedVehicle.GetComponent<Car_AI_Selected>();
            if (ai != null)
                ai.Go();
        }
    }

    public void SendStopCommand()
    {
        if (selectedVehicle != null)
        {
            var ai = selectedVehicle.GetComponent<Car_AI_Selected>();
            if (ai != null)
                ai.StopByHand();
        }
    }

    public void TrySelectVehicle()
    {
        if (rayOrigin == null) return;

        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.yellow);

        if (Physics.Raycast(ray, out hit, rayLength, vehicleLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject != selectedVehicle)
            {
                if (selectedVehicle != null)
                    selectedVehicle.GetComponent<Car_AI_Selected>()?.Deselect();

                if (hitObject.GetComponent<Car_AI_Selected>())
                {
                    selectedVehicle = hitObject;
                    selectedVehicle.GetComponent<Car_AI_Selected>().Select();
                }
            }
        }
    }
}