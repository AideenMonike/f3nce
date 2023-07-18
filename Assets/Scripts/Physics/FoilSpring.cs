using UnityEngine;

public class FoilSpring : MonoBehaviour
{
    // Variables needed are: 
    // Rigidbody component for physics stuff
    // The gameobjects themselves but more importantly they're transform components
    // The tip's previous position to make it flexy
    // Stuff to control how the foil bends
    public Rigidbody rb;
    public GameObject tip, _base, joint;
    private Vector3 tipPrevPos;
    public float springStrength, damperStrength, flexibility;

    // Start is called before the first frame update, and sets up the variables before runtime
    void Start()
    {
        rb = joint.GetComponent<Rigidbody>();
        tipPrevPos = tip.transform.position;
    }

    // Update is called once per frame, when physics is calculated. Applies the following methods in order to get the spring to rotate
    void FixedUpdate()
    {
        flexibility = PlayerPrefs.GetFloat("FlexLevel");
        ApplyRotate();
        PseudoSprings();
        LockJoint();
    }

    // The name derived from the fact that it seems like a 'fake' spring joint from unity
    // This essentially uses the cross product to find the torque perpendicular to where the bend is
    // then adds damper strength based how fast the joint is spinning
    private void PseudoSprings()
    {        
        Vector3 springTorque = springStrength * Vector3.Cross(tip.transform.right, _base.transform.right);
        Vector3 dampTorque = damperStrength * -rb.angularVelocity;
        rb.AddTorque(springTorque + dampTorque, ForceMode.Acceleration);    
    }

    // In theory the rigidbody component can lock the position relative to the parent, but this is for safe measure
    private void LockJoint()
    {
        Vector3 LocalJoint = transform.InverseTransformPoint(joint.transform.position);
        LocalJoint = new Vector3 (-0.1f, 0, 0);
        joint.transform.position = transform.TransformPoint(LocalJoint);
    }

    // Uses similar logic to the spring, using cross product to move based on the displacement of the foil tip
    private void ApplyRotate()
    {
        Vector3 tipDisplacement = tip.transform.position - tipPrevPos;
        Vector3 movementTorque = flexibility * Vector3.Cross(tipDisplacement, tip.transform.right);
        rb.AddTorque(movementTorque, ForceMode.Impulse);

        tipPrevPos = tip.transform.position;   
    }
}
