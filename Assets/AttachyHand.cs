using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachyHand : MonoBehaviour
{
    public GameObject Connect;
    public GameObject GrabObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GrabObject.transform.position = Connect.transform.position;
    }
}
