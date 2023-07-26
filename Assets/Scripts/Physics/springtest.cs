using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class springtest : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject tip, _base, joint;
    private Vector3 tipPrevPos, basePrevPos;
    private Vector3 baseVel;
    private Vector3 basePrevVel, basePrevAccel;
    public float springStrength, damperStrength, flexibility;

    // Start is called before the first frame update
    void Start()
    {
        rb = joint.GetComponent<Rigidbody>();
        Debug.Log("up " + Vector3.up);
        Debug.Log("tip " + tip.transform.up);
        Debug.Log("base " + _base.transform.up);
        tipPrevPos = tip.transform.position;
        basePrevVel = Vector3.zero;
        basePrevPos = _base.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyRotate();
        PseudoSprings();
        LockJoint();
    }

    private void PseudoSprings()
    {
        Debug.Log("tip up " + tip.transform.up);
        Debug.Log("base up "+ _base.transform.up);
        Debug.Log("angular vel "+ -rb.velocity);
        
        Vector3 springTorque = springStrength * Vector3.Cross(tip.transform.up, _base.transform.up);
        Vector3 dampTorque = damperStrength * -rb.angularVelocity;
        rb.AddTorque(springTorque + dampTorque, ForceMode.Acceleration);    
    }

    private void LockJoint()
    {
        Vector3 LocalJoint = transform.InverseTransformPoint(joint.transform.position);
        LocalJoint = new Vector3 (0, 1, 0);
        joint.transform.position = transform.TransformPoint(LocalJoint);
    }

    void ApplyRotate()
    {
        Vector3 tipDisplacement = tip.transform.position - tipPrevPos;
        Vector3 movementTorque = flexibility * Vector3.Cross(tipDisplacement, tip.transform.up);
        rb.AddTorque(movementTorque, ForceMode.Impulse);

        tipPrevPos = tip.transform.position;   
    }
}
