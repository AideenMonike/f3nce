using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFootIK : MonoBehaviour
{
    //Setting variables. animator controls the animator component, while the separate floats are limited in value from 0-1,
    //holding values that determine to what extent the Inverse Kinematics changes apply.
    private Animator animator;
    [Range(0,1)]
    public float rightFootPosWeight = 1;
    [Range(0,1)]
    public float leftFootPosWeight = 1;
    [Range(0,1)]
    public float rightFootRotWeight = 1;
    [Range(0,1)]
    public float leftFootRotWeight = 1;

    public Vector3 footOffset;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnAnimatorIK(int layerIndex)
    {
        /*
            The following is under the method "OnAnimatorIK", which sets the Inverse Kinematics animations. 
            It uses a raycast to measure where each individual foot is relative to the ground, and transforms
            the feet to accommodate for changes in the environment. 
        */
        Vector3 rightFootPos = animator.GetIKPosition(AvatarIKGoal.RightFoot);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(rightFootPos + Vector3.up, Vector3.down, out hit);

        if (hasHit)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootPosWeight);
            animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + footOffset);

            Quaternion rightFootRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightFootRotWeight);
            animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRotation);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
        }

        
        Vector3 leftFootPos = animator.GetIKPosition(AvatarIKGoal.LeftFoot);
        bool hasHitL = Physics.Raycast(leftFootPos + Vector3.up, Vector3.down, out hit);

        if (hasHit)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootPosWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + footOffset);

            Quaternion leftFootRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftFootRotWeight);
            animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRotation);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
        }
    }
}