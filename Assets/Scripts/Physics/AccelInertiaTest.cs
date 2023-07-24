using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*"AccelInertiaTest" script is used to perform motion calculations and apply spring-like 
torques to the Foil it is attached. 
The script calculates the velocity and acceleration of two Transform components (_base and tip)
and uses these calculations to apply spring-like torques to the Rigidbody component (rb) attached 
to the same Foil*/
public class AccelInertiaTest : MonoBehaviour
{
    public Transform _base, tip; //These represent the base and tip points of the Foil physical object which motion will be tracked
    public Rigidbody rb;
    private Vector3 basePrevPos, tipPrevPos; //These variables are used to calculate the velocity and acceleration of the _base and tip Transforms
    [SerializeField] private Vector3 baseVel, tipVel; //These variables store the current velocities of the _base and tip Transform
    private Vector3 basePrevVel, tipPrevVel; // These variables are used to calculate the acceleration of the _base and tip Transforms.
    [SerializeField] private Vector3 baseAccel, tipAccel; //These variables store the current accelerations of the _base and tip Transforms
    private Vector3 defaultTipPos; //This value is used in the SpringyBoingy() method to apply spring-like torques to the Foil.
    public float springStrength, damperStrength; //These values control the intensity of the spring-like torques applied to the Foil.
    // Start is called before the first frame update
    void Start()
    {
        /*Initializes various variables, such as previous positions and velocities 
        of the _base and tip Transforms, and sets the defaultTipPos value.*/
        tipPrevPos = tip.position;
        basePrevPos = _base.position;

        basePrevVel = baseVel;
        tipPrevVel = tipVel;

        defaultTipPos = transform.InverseTransformPoint(tip.position);

        rb = GetComponent<Rigidbody>();
    }


    /*"Update" script calls the MotionCalculations() method to calculate the
     velocity and acceleration of the _base and tip Transforms,
      and then calls the SpringyBoingy() method to apply spring-like torques
       to the Foil.*/
    // Update is called once per frame
    void Update()
    {
        MotionCalculations();
        SpringyBoingy();
    }

    /*"SpringyBoingy""calculates and applies the spring torque based on the difference
    between the Foils's up direction and the world up direction. This gives the object a
    springy behavior around its up axis.*/
    private void SpringyBoingy()
    {
        /* Quaternion rotation = Quaternion.FromToRotation(-tip.transform.forward, defaultTipPos);
        rb.AddTorque(new Vector3 (rotation.x, rotation.y, rotation.z) * springStrength);
        Debug.Log(rb.rotation); */
        Vector3 springTorque = springStrength * Vector3.Cross(rb.transform.up, Vector3.up);
        rb.AddTorque(springTorque, ForceMode.Acceleration);
    }

    /*"MotionCalculations" updates the current and previous positions and velocities of the _base 
    and tip Transforms and calculates their respective velocities and accelerations based on the position 
    changes over time.*/
    private void MotionCalculations()
    {
        tipPrevPos = tip.position;
        basePrevPos = _base.position;

        basePrevVel = baseVel;
        tipPrevVel = tipVel;

        tipVel = (tip.position - tipPrevPos) / Time.deltaTime;
        baseVel = (_base.position - basePrevPos) / Time.deltaTime;

        tipAccel = (tipVel - tipPrevVel) / Time.deltaTime;
        baseAccel = (baseVel - basePrevVel) / Time.deltaTime;
    }
}
