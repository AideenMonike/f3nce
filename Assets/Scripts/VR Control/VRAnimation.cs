using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/*"VRAnimation" script is attached to a game object in a virtual reality environment. 
 It is used to animate the game object's animator based on the movement of the VR headset.*/
public class VRAnimation : MonoBehaviour
{
    public float speedThreshold = 0.1f;
    [Range(0,1)]
    public float smoothing = 1;
    private Animator anim;
    private Vector3 previousPos;
    private VRRig vrRig;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        vrRig = GetComponent<VRRig>();
        previousPos = vrRig.head.vrTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Compute the speed
        Vector3 headsetSpeed = (vrRig.head.vrTarget.position - previousPos) / Time.deltaTime;
        headsetSpeed.x = 0;
        //Local Speed
        Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed);
        previousPos = vrRig.head.vrTarget.position;

        //Set Animator Values
        float previousDirectionX = anim.GetFloat("DirectionX");

        anim.SetBool("isMoving", headsetLocalSpeed.magnitude > speedThreshold);
        anim.SetFloat("DirectionX", Mathf.Lerp(previousDirectionX, Mathf.Clamp(headsetLocalSpeed.x, -1, 1), smoothing));
    }
}