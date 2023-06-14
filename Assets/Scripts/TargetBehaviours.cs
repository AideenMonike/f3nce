using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetBehaviours : MonoBehaviour
{
    private sorts Sort = new sorts();
    private Vector3 foilVel;
    private Vector3 prevFoilVel;
    [SerializeField]
    private Transform foilPos;
    private bool timeFinished = true;
    public TextMeshProUGUI timerText;
    public float GameTime;
    private float seconds, minutes;
    private string secondsText = "00";
    public float disappearTimer;
    public Vector3 origin;
    public float spawnRadius;
    public bool targetHit;
    private int score;
    private int[] scores = new int[4];
    public TextMeshProUGUI ScoreText, SavesText1, SavesText2, SavesText3;
    private string SaveHold;
    [Range(0, 3)]
    private int scoreIncrement = 0;
    private Vector3 originalTargPos;

    // Start is called before the first frame update
    void Start()
    {
        prevFoilVel = foilPos.position;
        originalTargPos = transform.position;
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i] = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Foil"  && foilVel.magnitude >= 5)
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
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Foil")
        {
            targetHit = false;
        }
    }

    private void Update()
    {
        ScoreText.text = "Score: " + Convert.ToString(score);
        timerText.text = Convert.ToString(minutes) + ":" + secondsText;

        if (timeFinished)
        {
            StopCoroutine(GameTimer(GameTime));
        }

        foilVel = (prevFoilVel - foilPos.position) / Time.deltaTime;
        prevFoilVel = foilPos.position;
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
            transform.position = originalTargPos;
            SortScore(score);
        }
    }

    void SortScore(int lastScore)
    {
        scores[scoreIncrement] = lastScore;
        scores = sorts.BubbleSort(scores);

        scoreIncrement++;
        if (scoreIncrement >= 3)
        {
            scoreIncrement = 3;
        }

        for (int i = 0; i < scores.Length; i++)
        {
            Debug.Log(scores[i]);
        }
        for (int j = 0; j <= 2; j++)
        {
            SaveHold = Convert.ToString(scores[j]);
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
        if (!timeFinished)
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
