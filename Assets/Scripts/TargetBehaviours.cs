using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class TargetBehaviours : MonoBehaviour
{
    public GameObject target;
    private Rigidbody rb;
    public float disappearTimer;
    public float totalTimer;
    public Vector3 origin;
    public float spawnRadius;
    private bool gameFinished;
    private bool targetHit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Timer targetTimer = new Timer(disappearTimer);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Foil")
        {
            if (!gameFinished)
            {

            }
            else
            {

            }
        }
    }
    private IEnumerator TargetSpawn()
    {
        transform.position = origin + Random.insideUnitSphere * spawnRadius;
        yield return "Target Spawned";
    }
}
