using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BezierCurve : MonoBehaviour
{
    public GameObject tip;

    public GameObject _base;

    //public Vector3 point1, point2;

    public Transform mPoint;
    private Vector3 midPoint;

    public int boneCount;

    private Vector3[] positions;

    public float cPCoefficient = 0.5f;

    private Vector3 tipPrevPos;

    // Unclear whether needed

    private Vector3 basePrevPos;

    private Vector3 tipVel;
    private Vector3 baseVel;
    public LineRenderer line;

    public void Start()
    {
        // Gets the starting position of the tip (& base) of the foil
        tipPrevPos = tip.transform.position;

        basePrevPos = _base.transform.position;
        positions = new Vector3[boneCount];
    }

   

    public void Update()
    {
        CalcFunction(_base.transform.position, tip.transform.position);
        SortPoints();
    }

    

    private void CalcFunction(Vector3 point1, Vector3 point2)
    {
        tipVel = (point2 - tipPrevPos) / Time.deltaTime;
        baseVel = (point1 - basePrevPos) / Time.deltaTime;
        midPoint = tipVel.magnitude * ((point1 + point2) / 2);
    }
   

    private void SortPoints()
    {
        if (tipVel.magnitude >= 5)
        {
            for (int i = 1; i < boneCount + 1; i++)
            {
                float t = i / (float)boneCount;
                positions[i - 1] = CalculateBezierCurvePoint(t, _base.transform.position, tip.transform.position, midPoint);
                Debug.Log(positions[i-1]);
            }
            line.SetPositions(positions);
        }
    }

    private Vector3 CalculateBezierCurvePoint(float t, Vector3 point1, Vector3 point2, Vector3 midP)
    {
        return (point1 * Mathf.Pow((1 - t), 2)) + (2 * (1 - t) * t * midP) + (Mathf.Pow(t, 2) * point2);
    } 
}
