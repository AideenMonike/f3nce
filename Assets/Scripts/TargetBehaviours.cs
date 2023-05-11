using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetBehaviours : MonoBehaviour
{
    private sorts Sort = new sorts();
    private bool timeFinished = true;
    public TextMeshProUGUI timerText;
    public float GameTime;
    private float seconds;
    private float minutes;
    private string secondsText;
    public float disappearTimer;
    public Vector3 origin;
    public float spawnRadius;
    [NonSerialized] public bool targetHit;
    private int score;
    int[] rawScoreSaves = new int[4];
    private int[] OrderedScores = new int[4];
    private int[] scores = new int[4];
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI SavesText1;
    public TextMeshProUGUI SavesText2;
    public TextMeshProUGUI SavesText3;
    private string SaveHold;
    [Range(0,3)]
    private int i = 0;
    private Vector3 originalTargPos;

    // Start is called before the first frame update
    void Start()
    {
        originalTargPos = transform.position;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Foil")
        {
            targetHit = true;
            if (timeFinished)
            {
                timeFinished = false;
                score = 0;
                TeleportTarget(origin, spawnRadius);
                StartCoroutine(GameTimer(GameTime));
                StartCoroutine(TargetTimer(disappearTimer));
            }

            if (!timeFinished)
            {
                score++;
            }
        }
    }

    private void Update()
    {
        ScoreText.text = "Score: " + Convert.ToString(score);
        timerText.text = Convert.ToString(minutes) + ":" + secondsText;        
    }

    private IEnumerator GameTimer(float time)
    {
        while (!timeFinished)
        {
            //Debug.Log(rawScoreSaves[0]);
            if (time <= 0)
            {
                timeFinished = true;
            }
            
            time -= Time.deltaTime;
            minutes = Mathf.Round(time / 60);
            seconds = Mathf.Round(time % 60);
        
            if (seconds < 10)
            {
                secondsText = "0" + Convert.ToString(seconds);
            }
            else
            {
                secondsText = Convert.ToString(seconds);
            }

            yield return new WaitForEndOfFrame();
        }
        if (timeFinished)
        {
            Debug.Log("timer line called");
            transform.position = originalTargPos;
            SortScore(score);
        }
        
        //yield return null;
    }
    void SortScore(int lastScore)
    {
        int g = 0;
        Debug.Log("i: " + i);
        timeFinished = true;
        rawScoreSaves[g] = lastScore;
        OrderedScores = sorts.InsertSort(rawScoreSaves);


        /*     
        for (int j = 0; j < rawScoreSaves.Length; j++)
        {
            Debug.Log(rawScoreSaves[j]);
        }
        */

        for (int j = 0; j <= 2; j++)
        {
            SaveHold = Convert.ToString(OrderedScores[j]);
            switch (j)
            {
               case 0:
                   SavesText1.text = SaveHold;
                   break;
                case 1:
                    SavesText2.text = SaveHold;
                    break;
                case 2:
                    SavesText3.text = SaveHold;
                    break;
            }
        }
                
        g++;
        if (g >= 3)
        {
            g = 3;
        }
    }
    private void TeleportTarget(Vector3 PO, float radius)
    {
        if (!timeFinished)
        {
            transform.position = PO + UnityEngine.Random.insideUnitSphere * radius;
        } 
    }
    private IEnumerator TargetTimer(float time)
    {
        while (!timeFinished)
        {
            while (time > 0)
            {
                if (targetHit == true)
                {
                    time = disappearTimer;
                    TeleportTarget(origin, spawnRadius);
                    targetHit = false;
                    yield return new WaitForSecondsRealtime(0.25f);
                    //Debug.Log("for reset");
                }
                yield return new WaitForEndOfFrame();
                //Debug.Log(time);
                time -= Time.deltaTime;
            }
            //Debug.Log("a");
            TeleportTarget(origin, spawnRadius);
            time = disappearTimer;
            yield return new WaitForEndOfFrame();
        }
    }
}
