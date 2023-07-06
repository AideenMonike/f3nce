using System;
using System.Collections;
using UnityEngine;

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

    public static string ScoreCalc(float[] scores, float size, int misses)
    {
        float average = AverageCalc(scores);

        float sizeMultiplier = size / 0.5f;
        float rawScore = ((10 / Mathf.Pow(average, 2)) * sizeMultiplier) - misses;
        int finScore = Mathf.RoundToInt(rawScore);

        return finScore.ToString();
    }

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