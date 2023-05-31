using UnityEngine;

//[ExecuteInEditMode]
public class BezierCurve : MonoBehaviour
{
    // Vector3, Transforms and GameObjects variables. Information for the base, tip, control points and previous positions
    public GameObject tip, _base;
    [SerializeField] private Vector3 midPoint;    
    private Vector3 prevMidPoint;
    [SerializeField] private Vector3 controlPoint;
    private Vector3 tipPrevPos, basePrevPos;
    // Float variables to control the position and speed of the control point
    private float lerpMult;
    public float smoothing;
    // Theoretical velocities of the tip
    [SerializeField] private Vector3 tipVel;
    private Vector3 prevTipVel;
    // Array of bones for the foil, positions calculated by the bezier curve and array-related variable;
    [SerializeField] private Transform[] bones;
    private int boneCount;
    private int posUpperBound;
    private Vector3[] positions;
    public LineRenderer line;

    public void Start()
    {
        boneCount = bones.Length;
        positions = new Vector3[boneCount];
        posUpperBound = positions.GetUpperBound(0);
        positions[posUpperBound] = tip.transform.position;

        // Gets the starting position of the tip (& base) of the foil
        tipPrevPos = tip.transform.position;
        basePrevPos = _base.transform.position;
        prevTipVel = tipVel;

        prevMidPoint = (_base.transform.position + tip.transform.position) / 2;
    }

    public void Update()
    {
        tipVel = (tipPrevPos - tip.transform.position) / Time.deltaTime;
        lerpMult = Mathf.Lerp(Mathf.Abs(prevTipVel.magnitude) / 2, Mathf.Abs(tipVel.magnitude) / 2, smoothing);
        CalcControlPoint();
        SortPoints();
        tipPrevPos = tip.transform.position;
        prevTipVel = tipVel;
    }

    // Need to lock midpoint/control point when moving along foil axis
    // something to do with local axis???
    // Also need to smooth the movement of the control point
    void CalcControlPoint()
    {
        midPoint = (_base.transform.position + tip.transform.position) / 2;
        controlPoint = Vector3.LerpUnclamped(midPoint, prevMidPoint, 1 * lerpMult);
        prevMidPoint = midPoint;
    }

    // Maybe add a conditional statement dictating that the bezier calculations happen after a huge difference in velocity
    private void SortPoints()
    {
        for (int i = 1; i < bones.Length + 1; i++)
        {
            float t = i / (float)boneCount;
            positions[i - 1] = CalculateBezierCurvePoint(t, _base.transform.position, tip.transform.position, controlPoint);
        }
        line.SetPositions(positions);

        for (int i = 0; i < positions.Length; i++)
        {
            bones[i].position = positions[i];
        }
    }
    private Vector3 CalculateBezierCurvePoint(float t, Vector3 point1, Vector3 point2, Vector3 midP)
    {
        float tt = t * t;  
        float u = 1 - t;
        float uu = u * u;
        float _2ut = 2 * u * t;

        return ((point1 * uu) + (_2ut * midP) + (tt * point2));
    } 
}