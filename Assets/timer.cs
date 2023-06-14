using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour
{
    public float timeRemaining = 10;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    IEnumerator Timer()
    {
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
    }
}
