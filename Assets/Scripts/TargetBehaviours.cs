using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class TargetBehaviours : MonoBehaviour
{
    public GameObject target;
    //private Rigidbody rb;
    public float disappearTimer;
    public float totalTimer;
    public Vector3 origin;
    public float spawnRadius;
    private bool gameFinished;
    private bool targetHit;
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //StartCoroutine(TargetHit());
        gameFinished = false;
        StartCoroutine(Timer());
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Foil")
        {
            targetHit = true;
            if (gameFinished)
            {
                Debug.Log("timer done");
            }
            else
            {
                
            }
        }
    }
    
    private IEnumerator Timer()
    {
        while (!gameFinished)
        {
            totalTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    /*
    private IEnumerator TargetHit()
    {
        Debug.Log(Time.captureFramerate);
        for (int i = 0; i <= Time.captureFramerate * disappearTimer; i++)
        {
            if (targetHit)
            {
                i = 0;
                StartCoroutine(TargetSpawn());
            }
            else if (!targetHit && i == (Time.captureFramerate * disappearTimer))
            {
                StartCoroutine(TargetSpawn());
                i = 0;
            }
        }
        yield return null;
    }
    /*
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(disappearTimer);
    }
    
    private IEnumerator TargetSpawn()
    {
        Debug.Log("A");
        transform.position = origin + Random.insideUnitSphere * spawnRadius;
        yield return null;
    }
    */
}
