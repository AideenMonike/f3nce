using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierEquationsCalculations : MonoBehaviour
{
    public Vector3 CalculateBezierCurvePoint(float t, Vector3 point1, Vector3 point2, Vector3 midP)
    {
        float tt = t * t;  
        float u = 1 - t;
        float uu = u * u;
        float _2ut = 2 * u * t;

        return ((point1 * uu) + (_2ut * midP) + (tt * point2));
    }

    public Vector3 CalculateBezierLinePoint(float t, Vector3 point1, Vector3 point2)
    {
        float u = 1 - t;

        return (u * point1) + t * point2;
    }

    public bool FloatValueCloseEnough(float a, float b, float maxValueApart)
    {
        if ((a - b) <= maxValueApart) return true;
        else return false;
    }
}
