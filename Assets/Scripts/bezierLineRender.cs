using UnityEngine;

[ExecuteInEditMode]
public class bezierLineRender : MonoBehaviour
{
    public Transform point1, point2;
    public Vector3 tPoint;
    public Transform mPoint;
    public Vector3 preMPoint;
    private float lerpMult;
    private Vector3 prevTipPos, tipVel;
    private Vector3[] positions;
    public int sectionCount;
    public float smoothing;
    public LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        positions = new Vector3[sectionCount];
        preMPoint = mPoint.position;
        prevTipPos = point1.position;
    }

    // Update is called once per frame
    void Update()
    {   
        tipVel = (prevTipPos - point2.position) / 2;
        lerpMult = Mathf.Lerp(0, Mathf.Abs(tipVel.magnitude), smoothing);
        Point3();
        SortPoints();
    }
    void Point3()
    {
        mPoint.position = (point1.position + point2.position) / 2;
        tPoint = Vector3.LerpUnclamped(mPoint.position, preMPoint, 1 * lerpMult);
        preMPoint = mPoint.position;
    }
    private void SortPoints()
    {
        for (int i = 1; i < sectionCount + 1; i++)
        {
            float t = i / (float)sectionCount;
            positions[i - 1] = CalculateBezierCurvePoint(t, point1.position, point2.position, tPoint);
        }
        line.SetPositions(positions);
    }
    private Vector3 CalculateBezierCurvePoint(float t, Vector3 point1, Vector3 point2, Vector3 midP)
    {
        return (point1 * Mathf.Pow((1 - t), 2)) + (2 * (1 - t) * t * midP) + (Mathf.Pow(t, 2) * point2);
    } 
}
