using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class TargetBehaviours : MonoBehaviour
{
    public GameObject target;
    public float disappearTimer;
    public float totalTimer;
    public Vector3 origin;
    public float spawnRadius;
    private bool gameFinished = true;
    private bool targetHit;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Foil")
        {
            targetHit = true;
            if (gameFinished)
            {
                TeleportTarget(origin, spawnRadius);
                StartCoroutine(GameTimer(totalTimer));
                StartCoroutine(TargetTimer(disappearTimer));
            }
        }
    }
    private void TeleportTarget(Vector3 PO, float radius)
    {
        transform.position = PO + Random.insideUnitSphere * radius;
    }
    private IEnumerator GameTimer(float time)
    {
        while (time < 0)
        {
            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        gameFinished = false;
    }
    private IEnumerator TargetTimer(float time)
    {
        while (!gameFinished)
        {
            while (time > 0)
            {
                if (targetHit == true)
                {
                    time = disappearTimer;
                    targetHit = false;
                    TeleportTarget(origin, spawnRadius);
                    Debug.Log("for reset");
                }
                yield return new WaitForEndOfFrame();
                Debug.Log(time);
                time -= Time.deltaTime;
            }
            //Debug.Log("a");
            TeleportTarget(origin, spawnRadius);
            time = disappearTimer;
            yield return new WaitForEndOfFrame();
        }
    }
}
