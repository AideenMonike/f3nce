using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*This code defines a VRRig system that maps the positions and rotations 
 of VR targets (e.g., head and hands) to rig targets (e.g., camera and hand
 controllers) in a VR environment. The VRRig ensures that the camera and hand 
 controllers follow the VR targets while applying any specified offsets to their 
 positions and rotations.*/
 
[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;
  

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class VRRig : MonoBehaviour
{
    public VRMap head;
    public VRMap rightHand;
    public VRMap leftHand;

    public Transform headConstraint;
    public Vector3 headBodyOffset;

    public int turnSmoothness = 5;

    // Start is called before the first frame update
    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = headConstraint.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward,
        Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized,Time.deltaTime * turnSmoothness);

        head.Map();
        rightHand.Map();
        leftHand.Map();
    }
}
