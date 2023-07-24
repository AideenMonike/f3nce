using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*"CubeFly" controls the movement of a cube towards a target GameObject using physics-based forces */
public class CubeFly : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float disX = transform.position.x - target.transform.position.x;
        float disY = transform.position.y - target.transform.position.y;
        float disZ = transform.position.z - target.transform.position.z;
        Vector3 force = new Vector3(-disX, -2*disY, -disZ);
        rb.AddForce(force);
        //rb.AddTorque(0, 100f, 0);
        
    }
}
