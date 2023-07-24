using UnityEngine;

/*"BezierCurve" script is responsible for calculating positions along a quadratic Bezier curve and applying the 
calculated positions to the Foil bones (Transform components) in the Unity scene. The script also handles tip inertia 
based on the position, velocity, and acceleration of the "tip" of the Foil*/

public class BezierCurve : MonoBehaviour
{
    public BezierEquationsCalculations bez;
    // Vector3, Transforms and GameObjects variables. Information for the base, tip, control points and previous positions
    public GameObject tip, _base;
    [SerializeField] private Vector3 midPoint;    
    private Vector3 prevMidPoint;
    [SerializeField] private Vector3 theoryTip;
    private Vector3 localTip;
    [SerializeField] private Vector3 controlPoint, prevControlPoint;
    private Vector3 tipPrevPos, basePrevPos;
    // Float variables to control the position and speed of the control point
    private float lerpMult;
    [Range (0, 1)] public float smoothing;
    // Theoretical velocities of the tip
    [SerializeField] private Vector3 tipVel, baseVel;
    private Vector3 prevTipVel, prevBaseVel;
    [SerializeField] private Vector3 tipAccel, baseAccel;
    private Vector3 prevBaseAccel, prevTipAccel;
    private bool noBendFoil;
    // Array of bones for the foil, positions calculated by the bezier curve and array-related variable;
    [SerializeField] private Transform[] bones;
    private int boneCount;
    private int posLineUpperBound, posCurveUpperBound;
    private Vector3[] curvePositions, linePositions;

    public float velThreshold;

/*"Start" script initializes various variables, such as previous positions and velocities of 
the "tip" and "_base" game objects, as well as the positions array for the Foil bones.*/

    public void Start()
    {
        // Sets all the variables for the arrays used
        boneCount = bones.Length;

        linePositions = new Vector3[boneCount];
        posLineUpperBound = linePositions.GetUpperBound(0);
        linePositions[posLineUpperBound] = tip.transform.position;
        
        curvePositions = new Vector3[boneCount];
        posCurveUpperBound = curvePositions.GetUpperBound(0);
        curvePositions[posCurveUpperBound] = tip.transform.position;

        // Gets the starting position of various positions and Vector3s
        tipPrevPos = tip.transform.position;
        basePrevPos = _base.transform.position;
        prevTipVel = tipVel;
        prevMidPoint = (_base.transform.position + tip.transform.position) / 2;
        prevControlPoint = controlPoint;

        theoryTip = transform.InverseTransformPoint(tip.transform.position);

        bez = new BezierEquationsCalculations();
    }
    /*"Update" script calls several other methods to perform the calculations for velocity,
     acceleration, control point, tip inertia, and position of the bones along the Bezier curve.*/
    public void Update()
    {
        CalcPointVelocities();
        CalcControlPoint();
        TipInertia();

        SortPoints();

        SetPrevMotionValues();
    }
    
    private void SetPrevMotionValues()
    {
        // Previous Position variables set
        tipPrevPos = tip.transform.position;
        prevTipVel = tipVel;
        basePrevPos = _base.transform.position;
        prevBaseVel = baseVel;
    }

    // Calculates theoretical velocities using the equation v = s/t, where s is the displacement and t is time.
    // ALso calculates the control point's position using lerpMult (lerp multiplier).
    private void CalcPointVelocities()
    {
        // Velocity Calculations
        tipVel = (tip.transform.position - tipPrevPos) / Time.deltaTime;
        baseVel = (_base.transform.position - basePrevPos) / Time.deltaTime;
        lerpMult = Mathf.Abs(tipVel.magnitude) / 100;
    }
    /*"TipInertia" calculates the acceleration of the "tip" and "_base" game objects and determines if the "foil" should
     bend or not based on certain conditions*/
    private void TipInertia()
    {
        // Acceleration Calculations
        tipAccel = (tip.transform.position - tipPrevPos) / Mathf.Pow(Time.deltaTime, 2);
        baseAccel =  (_base.transform.position - basePrevPos) / Mathf.Pow(Time.deltaTime, 2);
        // Determine direction of tip by acceleration(?) - sudden change in accel and can scalar multiply it by -1
        // Change time that tip 
        if (!(!noBendFoil && tipVel.magnitude >= 20f))
        {
            noBendFoil = true;
        }
        else if (tipAccel.magnitude > 5 * prevTipAccel.magnitude || (!noBendFoil && tipVel.magnitude >= 20f))
        {
            noBendFoil = false;
            tip.transform.position = Vector3.LerpUnclamped(tip.transform.position, tipPrevPos, tipVel.magnitude);
            localTip = transform.InverseTransformPoint(tip.transform.position);
            Debug.Log(theoryTip);
            Debug.Log(localTip);
            
            float mult = LocalZMultiplier(theoryTip, localTip);
            Debug.Log(mult);
            localTip.z *= mult;
            tip.transform.position = Vector3.Lerp(tip.transform.position, transform.TransformPoint(localTip), smoothing);
        }
        
        prevTipAccel = tipAccel;
        prevBaseAccel = baseAccel;
    }
    /*"LocalZMultiplier" calculates a multiplier based on the positions of the "tip" game object in the 
    global and local space of the GameObject and helps determine if the "foil" should bend or not*/
    private float LocalZMultiplier(Vector3 statA, Vector3 b)
    {
        float bZ = Mathf.Pow(b.z, 2);
        float bX = Mathf.Pow(b.x, 2);
        float bY = Mathf.Pow(b.y, 2);

        float multiplier = (Mathf.Pow(statA.magnitude, 2) - bX - bY) / bZ;
        if (Mathf.Approximately(statA.magnitude, Mathf.Sqrt(bX + bY + (multiplier * bZ) )) )
        {
            return multiplier;
        }
        else return 1f;
    }

    // Need to lock midpoint/control point when moving along foil axis
    // something to do with local axis???

    // FOR SMOOTHING
    // All it required was a lerp from it's previous control point to it's current. Since LerpUnclamped
    // Was moreso for extrapolition over an animation curve, using Lerp actually affected how it ran.
    // A vector projection was also used so that the control point would be close to above the midpoint of the foil.
    private void CalcControlPoint()
    {
        midPoint = (_base.transform.position + tip.transform.position) / 2;
        /* controlPoint = Vector3.LerpUnclamped(midPoint, prevMidPoint, lerpMult);
        controlPoint = Vector3.Lerp(prevControlPoint, Vector3.Project(midPoint, controlPoint), smoothing); */
        controlPoint = Vector3.Lerp(prevMidPoint, midPoint, smoothing);

        prevMidPoint = midPoint;
        prevControlPoint = controlPoint;
    }

    // Calculates t so it will always be an evenly distributed fraction.
    // All positions are already in the global space.
    private void SortPoints()
    {
        for (int i = 1; i < bones.Length + 1; i++)
        {
            float t = i / (float)boneCount;
            // IF ELSE FOR LINEAR OR QUADRATIC
            curvePositions[i - 1] = bez.CalculateBezierCurvePoint(t, _base.transform.position, tip.transform.position, controlPoint);
            //linePositions[i - 1] = bez.CalculateBezierLinePoint(t, _base.transform.position, tip.transform.position);
        }
    
        for (int i = 0; i < curvePositions.Length; i++)
        {
            bones[i].position = curvePositions[i];
        }
    }
    
    // Calculates positions along a curve using the quadratic bezier curve equation. Variable t is clamped within 0 to 1.
    // Alternatively, a cubic bezier curve can be used, but that would necessitate another control point, which for this
    // feature is unnecessary.
}