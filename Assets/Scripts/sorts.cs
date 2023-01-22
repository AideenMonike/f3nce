using System;
using System.Collections;
using UnityEngine;

public class sorts
{
    public static int[] InsertSort(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            int spot = arr[i];
            int j = i - 1;
            while (j >= 0 && spot < arr[j])
            {
                arr[i] = arr[j];
                j--;
            }
            arr[j + 1] = spot;
        }
        return arr;
    }
}