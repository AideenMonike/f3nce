using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightScaling : MonoBehaviour
{
    public Camera xrCam;
    public GameObject model;
    private float modelHeight = 1.77f;

    public void Resize()
    {
        float headHeight = xrCam.transform.localPosition.y;
        float scale = modelHeight / headHeight;
        transform.localScale = Vector3.one * scale;
        model.transform.localScale = Vector3.one * scale;
    }
}
