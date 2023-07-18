using UnityEngine;

[RequireComponent(typeof(Collision))]
public class FoilHitDetect : MonoBehaviour
{
    public onCollisionEnter onCol;
    private void OnCollisionEnter(Collision other)
    {
        onCol?.Invoke(other);
    }
}
