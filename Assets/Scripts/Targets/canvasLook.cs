using UnityEngine;

public class canvasLook : MonoBehaviour
{
    public GameObject canvas;
    public Transform head;
    [SerializeField] private float smoothing;
    public bool rotateToHead, moveToHead;

    // Update is called once per frame
    void Update()
    {
        if (rotateToHead)
        {
            canvas.transform.LookAt(new Vector3 (head.position.x, canvas.transform.position.y, head.position.z));
            canvas.transform.forward *= -1;
        }
        if (moveToHead)
        {
            canvas.transform.position = transform.TransformPoint(transform.InverseTransformPoint(new Vector3 (head.position.x + 0.5f, head.position.y, head.position.z)));
        }
    }
}
