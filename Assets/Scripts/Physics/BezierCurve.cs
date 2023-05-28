using UnityEngine;

//[ExecuteInEditMode]
public class BezierCurve : MonoBehaviour
{
    public GameObject tip;

    public GameObject _base;
    public Rigidbody rbTip, rbBase;
    public Transform midFoil;
    private Vector3 prevMidFoil;
    [SerializeField] private Transform midPoint;
    private Vector3 difMid;
    [SerializeField] private Transform[] bones;
    public int boneCount;

    private Vector3[] positions;
    private Vector3 tipPrevPos;
    private Vector3 basePrevPos;
    public LineRenderer line;

    public void Start()
    {
        // Gets the starting position of the tip (& base) of the foil
        tipPrevPos = tip.transform.position;
        basePrevPos = _base.transform.position;
        prevMidFoil = midFoil.position;
        positions = new Vector3[boneCount];
    }

    public void Update()
    {
        //CalcFunction(prevMidFoil, midFoil.position);
        SortPoints();
        Debug.Log(prevMidFoil);
        prevMidFoil = midFoil.position;
    }

    private void CalcFunction(Vector3 point1, Vector3 point2)
    {
        difMid = point2 + point1;
        midPoint.position = -difMid;
    }

    private void SortPoints()
    {
        for (int i = 1; i < 10 + 1; i++)
        {
            float t = i / (float)boneCount;
            positions[i - 1] = transform.InverseTransformPoint(CalculateBezierCurvePoint(t, transform.TransformPoint(_base.transform.position), transform.TransformPoint(tip.transform.position), midPoint.position));
        }
        //line.SetPositions(positions);
        for (int i = 0; i < 10; i++)
        {
            bones[i].position = positions[i];
        }
    }

    private Vector3 CalculateBezierCurvePoint(float t, Vector3 point1, Vector3 point2, Vector3 midP)
    {
        return (point1 * Mathf.Pow((1 - t), 2)) + (2 * (1 - t) * t * midP) + (Mathf.Pow(t, 2) * point2);
    } 
}
