using System;
using System.Collections;
using UnityEngine;

public class sorts
{
    /* public static int[] InsertSort(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            int spot = arr[i];
            int j = i - 1;
            while (j >= 0 && spot < arr[j])
            {
                arr[j+1] = arr[j];
                j--;
            }
            arr[j + 1] = spot;
        }
        return arr;
    } */
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
}