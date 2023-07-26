using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetBehaviours : MonoBehaviour
{
    private sorts Sort = new sorts();
    private bool timeFinished = true;
    public Text timerText;
    public float GameTime;
    private float seconds;
    private float minutes;
    private string secondsText;
    public float disappearTimer;
    public Vector3 origin;
    public float spawnRadius;
    public bool targetHit;
    private int score;
    int[] rawScoreSaves = new int[4];
    private int[] OrderedScores = new int[4];
    private int[] scores = new int[4];
    public Text ScoreText;
    public Text SavesText1;
    public Text SavesText2;
    public Text SavesText3;
    private Text SaveHold;
    [Range(0,3)]
    private int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        
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
                Debug.Log("i: " + i);
                time = 0;
                timeFinished = true;
                rawScoreSaves[i] = score;
                OrderedScores = rawScoreSaves;
                
                
                sorts.InsertSort(OrderedScores);
                Debug.Log("rawscoresaves");
                for (int j = 0; j < rawScoreSaves.Length; j++)
                {
                    Debug.Log(rawScoreSaves[j]);
                }
                /*
                Debug.Log("Orderedscores");                
                for (int i = 0; i < scores.Length; i++)
                {
                    Debug.Log(scores[i]);
                }
                */
                for (int j = 0; j <= 2; j++)
                {
                    switch (j)
                    {
                        case 0:
                            SaveHold = SavesText1;
                            break;
                        case 1:
                            SaveHold = SavesText2;
                            break;
                        case 2:
                            SaveHold = SavesText3;
                            break;
                    }
                    SaveHold.text = Convert.ToString(OrderedScores[3 - j]);
                }
                
                i++;
                if (i >= 3)
                {
                    i = 3;
                }

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
        
        //yield return null;
    }
    private void TeleportTarget(Vector3 PO, float radius)
    {
        transform.position = PO + UnityEngine.Random.insideUnitSphere * radius;
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
                    targetHit = false;
                    TeleportTarget(origin, spawnRadius);
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
