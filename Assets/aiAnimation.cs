using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiAnimation : MonoBehaviour
{
    public float speedThreshold = 0.1f;
    [Range(0,1)]
    public float smoothing = 1;
    private Animator anim;
    private Vector3 previousPos;
    public GameObject aiModel; 
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        previousPos = aiModel.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Get Speed
        Vector3 modelSpeed = (aiModel.transform.position - previousPos) / Time.deltaTime;
        modelSpeed.y = 0;
        
        //Gets the relative (local) speed of the model
        Vector3 localModelSpeed = transform.InverseTransformDirection(modelSpeed);
        previousPos = aiModel.transform.position;
        
        

        //Sets conditional boolean based upon the model's relative speed
        anim.SetBool("isMoving", localModelSpeed.magnitude > speedThreshold);
        anim.SetFloat("DirectionX", Mathf.Clamp(localModelSpeed.x, -1, 1));
    }
}
