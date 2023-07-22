using UnityEngine;

// Collision detector that differentiates different colliders using Unity Events
// Because Unity is slightly wacky serializable generic events don't exist so the events have been defined in CollisionEvents.cs
[RequireComponent(typeof(Collider))]
public class HitnMissColDetector : MonoBehaviour
{
    public onTriggerEnter enter;
    public onTriggerStay stay;
    public onTriggerExit exit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FoilTip"))
        {
            enter?.Invoke(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("FoilTip"))
        {
            stay?.Invoke(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FoilTip"))
        {
            exit?.Invoke(other);
        }
    }
}