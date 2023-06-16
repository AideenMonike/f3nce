using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasLook : MonoBehaviour
{
    public GameObject canvas;
    public Transform head;
    [SerializeField] private float smoothing;

    // Update is called once per frame
    void Update()
    {
        canvas.transform.LookAt(new Vector3 (head.position.x, canvas.transform.position.y, head.position.z));
        canvas.transform.forward *= -1;
    }
}
