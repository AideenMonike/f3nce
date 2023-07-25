using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*"HeightScaling" script is used to adjust the scale of an object based 
 on the height of the XR camera (e.g., VR/AR camera) to ensure that the object 
 appears at a consistent size relative to the user's head position.*/
public class HeightScaling : MonoBehaviour
{
    public Camera xrCam;
    public GameObject model;
    private float modelHeight = 1.77f;

    /* "Resize" function is responsible for resizing the GameObject to 
    match the user's head height.*/
    public void Resize()
    {
        float headHeight = xrCam.transform.localPosition.y; //Get the head height of the XR camera (headHeight) by accessing its local position's y-coordinate.
        float scale = modelHeight / headHeight; //Set the scale of the parent object
        transform.localScale = Vector3.one * scale;
        model.transform.localScale = Vector3.one * scale; //Set the scale of the "player" 
    }
}
