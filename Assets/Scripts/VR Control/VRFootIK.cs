using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*"VRFootIK" is attached to a game object with an Animator component. 
 It is used to implement inverse kinematics (IK) for the feet of a character 
 in a virtual reality environment. IK is used to make the character's feet 
 align with the ground surface, adjusting their positions and rotations dynamically 
 based on the terrain.*/
public class VRFootIK : MonoBehaviour
{
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
        Vector3 rightFootPos = animator.GetIKPosition(AvatarIKGoal.RightFoot);
        RaycastHit hit;

        bool hasHit = Physics.Raycast(rightFootPos + Vector3.up, Vector3.down, out hit);
        if (hasHit)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootPosWeight);
            //Debug.Log("hit l33");
            animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + footOffset);

            Quaternion rightFootRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightFootRotWeight);
            animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRotation);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
            //Debug.Log("hit l43");
        }

        
        Vector3 leftFootPos = animator.GetIKPosition(AvatarIKGoal.LeftFoot);
        bool hasHitL = Physics.Raycast(leftFootPos + Vector3.up, Vector3.down, out hit);

        if (hasHitL)
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
