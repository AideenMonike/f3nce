using UnityEngine;

public class FoilSpring : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject tip, _base, joint;
    private Vector3 tipPrevPos, basePrevPos;
    private Vector3 tipOrigin, baseOrigin, jointOrigin;
    public float springStrength, damperStrength, flexibility;

    // Start is called before the first frame update
    void Start()
    {
        rb = joint.GetComponent<Rigidbody>();
        Debug.Log("right " + Vector3.right);
        Debug.Log("tip " + tip.transform.right);
        Debug.Log("base " + _base.transform.right);
        tipPrevPos = tip.transform.position;
        
        
        tipOrigin = transform.InverseTransformPoint(tip.transform.position);
        baseOrigin = transform.InverseTransformPoint(_base.transform.position);
        jointOrigin = transform.InverseTransformPoint(joint.transform.position);
        Debug.Log($"this is base origin {baseOrigin}");
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
        /* Debug.Log("tip right " + tip.transform.right);
        Debug.Log("base right "+ _base.transform.right);
        Debug.Log("angular vel "+ -rb.velocity); */
        
        Vector3 springTorque = springStrength * Vector3.Cross(tip.transform.right, _base.transform.right);
        Vector3 dampTorque = damperStrength * -rb.angularVelocity;
        rb.AddTorque(springTorque + dampTorque, ForceMode.Acceleration);    
    }

    private void LockJoint()
    {
        Vector3 LocalJoint = transform.InverseTransformPoint(joint.transform.position);
        // LocalJoint = new Vector3 (0, 1, 0);
        LocalJoint = new Vector3 (-0.1f, 0, 0);
        joint.transform.position = transform.TransformPoint(LocalJoint);
    }

    private void ApplyRotate()
    {
        Vector3 tipDisplacement = tip.transform.position - tipPrevPos;
        Vector3 movementTorque = flexibility * Vector3.Cross(tipDisplacement, tip.transform.right);
        rb.AddTorque(movementTorque, ForceMode.Impulse);

        tipPrevPos = tip.transform.position;   
    }
}
