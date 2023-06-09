using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelInertiaTest : MonoBehaviour
{
    public Transform _base, tip;
    public Rigidbody rb;
    private Vector3 basePrevPos, tipPrevPos;
    [SerializeField] private Vector3 baseVel, tipVel;
    private Vector3 basePrevVel, tipPrevVel;
    [SerializeField] private Vector3 baseAccel, tipAccel;
    private Vector3 defaultTipPos;
    public float springStrength, damperStrength;
    // Start is called before the first frame update
    void Start()
    {
        tipPrevPos = tip.position;
        basePrevPos = _base.position;

        basePrevVel = baseVel;
        tipPrevVel = tipVel;

        defaultTipPos = transform.InverseTransformPoint(tip.position);

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MotionCalculations();
        SpringyBoingy();
    }

    private void SpringyBoingy()
    {
        /* Quaternion rotation = Quaternion.FromToRotation(-tip.transform.forward, defaultTipPos);
        rb.AddTorque(new Vector3 (rotation.x, rotation.y, rotation.z) * springStrength);
        Debug.Log(rb.rotation); */
        Vector3 springTorque = springStrength * Vector3.Cross(rb.transform.up, Vector3.up);
        rb.AddTorque(springTorque, ForceMode.Acceleration);
    }

    private void MotionCalculations()
    {
        tipPrevPos = tip.position;
        basePrevPos = _base.position;

        basePrevVel = baseVel;
        tipPrevVel = tipVel;

        tipVel = (tip.position - tipPrevPos) / Time.deltaTime;
        baseVel = (_base.position - basePrevPos) / Time.deltaTime;

        tipAccel = (tipVel - tipPrevVel) / Time.deltaTime;
        baseAccel = (baseVel - basePrevVel) / Time.deltaTime;
    }
}
