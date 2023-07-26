using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScoreCount : MonoBehaviour
{
    public bool timeFinished = true;
    public float timer;
    public Text timerText;
    private float seconds;
    private string secondsText;
    private float minutes;
    private TargetBehaviours Targ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Targ.targetHit && !timeFinished)
        {
            StartCoroutine(Timer());
        }

    }

    private IEnumerator Timer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0;
            timeFinished = true;
        }

        minutes = Mathf.Round(timer / 60);
        Debug.Log(minutes);
        seconds = Mathf.Round(timer % 60);
        Debug.Log(seconds);
        
        if (seconds < 10)
        {
            secondsText = "0" + Convert.ToString(seconds);  
        }
        else
        {
            secondsText = Convert.ToString(seconds);  
        }

        timerText.text = Convert.ToString(minutes) + ":" + secondsText;
        yield return null;
    }
}
