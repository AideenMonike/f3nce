using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    // Vector3, Transforms and GameObjects variables. Information for the base, tip, control points and previous positions
    public GameObject tip, _base;
    [SerializeField] private Vector3 midPoint;    
    private Vector3 prevMidPoint;
    [SerializeField] private Vector3 controlPoint, prevControlPoint;
    private Vector3 tipPrevPos, basePrevPos;
    // Float variables to control the position and speed of the control point
    private float lerpMult;
    [Range (0, 1)] public float smoothing;
    // Theoretical velocities of the tip
    [SerializeField] private Vector3 tipVel, baseVel;
    private Vector3 prevTipVel;
    private bool noBendFoil;
    // Array of bones for the foil, positions calculated by the bezier curve and array-related variable;
    [SerializeField] private Transform[] bones;
    private int boneCount;
    private int posLineUpperBound, posCurveUpperBound;
    private Vector3[] curvePositions, linePositions;

    public float velThreshold;

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
    }

    public void Update()
    {
        CalcPointVelocities();
        CalcControlPoint();
        noBendFoil = FloatValueCloseEnough(tipVel.magnitude, baseVel.magnitude, 5f);
        SortPoints();
    }
    
    // Calculates theoretical velocities using the equation v = s/t, where s is the displacement and t is time.
    // ALso calculates the control point's position using lerpMult (lerp multiplier).
    private void CalcPointVelocities()
    {
        //lerpMult = 0;
        tipVel = (tip.transform.position - tipPrevPos) / Time.deltaTime;
        baseVel = (_base.transform.position - basePrevPos) / Time.deltaTime;
        lerpMult = Mathf.Abs(tipVel.magnitude) / 100;

        tipPrevPos = tip.transform.position;
        prevTipVel = tipVel;
        basePrevPos = _base.transform.position;
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
        controlPoint = Vector3.LerpUnclamped(midPoint, prevMidPoint, lerpMult);
        controlPoint = Vector3.Lerp(prevControlPoint, Vector3.Project(midPoint, controlPoint), smoothing);
        
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
            curvePositions[i - 1] = CalculateBezierCurvePoint(t, _base.transform.position, tip.transform.position, controlPoint);
            linePositions[i - 1] = CalculateBezierLinePoint(t, _base.transform.position, tip.transform.position);
        }
    
        for (int i = 0; i < curvePositions.Length; i++)
        {
            bones[i].position = curvePositions[i];
        }
    }
    
    // Calculates positions along a curve using the quadratic bezier curve equation. Variable t is clamped within 0 to 1.
    // Alternatively, a cubic bezier curve can be used, but that would necessitate another control point, which for this
    // feature is unnecessary.
    private Vector3 CalculateBezierCurvePoint(float t, Vector3 point1, Vector3 point2, Vector3 midP)
    {
        float tt = t * t;  
        float u = 1 - t;
        float uu = u * u;
        float _2ut = 2 * u * t;

        return ((point1 * uu) + (_2ut * midP) + (tt * point2));
    }

    private Vector3 CalculateBezierLinePoint(float t, Vector3 point1, Vector3 point2)
    {
        float u = 1 - t;

        return (u * point1) + t * point2;
    }

    public static bool FloatValueCloseEnough(float a, float b, float maxValueApart)
    {
        if ((a - b) <= maxValueApart) return true;
        else return false;
    }

    /* public static bool Vector3CloseEnough(Vector3 a, Vector3 b, Vector3 maxValueApart)
    {
        if ((a - b) <= maxValueApart) return true;
    } */
}