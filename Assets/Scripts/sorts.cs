using System;
using System.Collections;
using UnityEngine;

/*Description: This function implements the Bubble Sort algorithm 
to sort an integer array in descending order.
Parameters:
arr: The input integer array that needs to be sorted.
Returns: An integer array with its elements sorted in descending order.*/
public class sorts
{
   

    public static int[] BubbleSort(int[] arr)
    {
        bool swapped = true;
        while (swapped)
        {
            swapped = false;
            for (int j = 0; j < arr.Length - 1; j++)
            {
                if (arr[j] < arr[j+1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j+1];
                    arr[j+1] = temp;
                    swapped = true;
                }
            }
        }
        return arr;
    }

 /*Description: This function calculates the final score based on an array of scores, 
    the size of an object, and the number of misses.
    Parameters:
        scores: An array of floating-point numbers representing individual scores achieved in a game or activity.
        size: A floating-point value representing the size of an object (difficulty multiplier).
        misses: An integer representing the number of misses made during the game/activity.
        Returns: A string containing the final score, calculated based on the input parameters.*/
    public static string ScoreCalc(float[] scores, float size, int misses)
    {
        float average = AverageCalc(scores);

        float sizeMultiplier = size / 0.5f;
        float rawScore = ((10 / Mathf.Pow(average, 2)) * sizeMultiplier) - misses;
        int finScore = Mathf.RoundToInt(rawScore);

        return finScore.ToString();
    }

/*Description: This function calculates the average value of an array of floating-point numbers.
    Parameters:
        scores: An array of floating-point numbers for which the average needs to be calculated.
        Returns: A floating-point value representing the average of the input array.*/
    public static float AverageCalc(float[] scores)
    {
        float average = 0;
        for (int i = 0; i < scores.Length; i++)
        {
            average = average + scores[i];
        }
        for (int i = 0; i < scores.Length; i++)
        {
            // Debug.Log($"pos {i} : {scores[i]}");
        }
        // Debug.Log($"length {scores.Length}");
        average = average / scores.Length;

        return average;
    }
}