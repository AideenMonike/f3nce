using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*"BezierEquationsCalculations." script contains three functions related to Bezier curves and 
float value comparison*/

public class BezierEquationsCalculations : MonoBehaviour
{
        /*"CalculateBezierCurvePoint" calculates a point on a quadratic Bezier curve based on the given time parameter (t) and 
         three control points: point1, point2, and midP.
            Parameters:
                t: The time parameter ranging from 0 to 1, representing the position along the curve.
                point1: The starting point of the Bezier curve.
                point2: The ending point of the Bezier curve.
                midP: The control point that influences the curve's shape.
                Return: A Vector3 representing the position on the quadratic Bezier curve at the given time parameter (t).*/
    public Vector3 CalculateBezierCurvePoint(float t, Vector3 point1, Vector3 point2, Vector3 midP)
    {
        float tt = t * t;  
        float u = 1 - t;
        float uu = u * u;
        float _2ut = 2 * u * t;

        return ((point1 * uu) + (_2ut * midP) + (tt * point2));
    }


        /*"CalculateBezierLinePoint" calculates a point on a linear Bezier line based on the given time parameter (t) and two control
         points: point1 and point2.
            Parameters:
                t: The time parameter ranging from 0 to 1, representing the position along the line.
                point1: The starting point of the Bezier line.
                point2: The ending point of the Bezier line.
                Return: A Vector3 representing the position on the linear Bezier line at the given time parameter (t).*/
    public Vector3 CalculateBezierLinePoint(float t, Vector3 point1, Vector3 point2)
    {
        float u = 1 - t;

        return (u * point1) + t * point2;
    }


        /*"FloatValueCloseEnough" function compares two floating-point values (a and b) to check if they are close enough within a specified 
         maximum difference (maxValueApart).
            Parameters:
            a: The first floating-point value for comparison.
            b: The second floating-point value for comparison.
            maxValueApart: The maximum allowed difference between a and b to be considered as "close enough."
            Return: A boolean value (true or false) indicating whether the difference between a and b is within the specified maxValueApart.*/
    public bool FloatValueCloseEnough(float a, float b, float maxValueApart)
    {
        if ((a - b) <= maxValueApart) return true;
        else return false;
    }
}
