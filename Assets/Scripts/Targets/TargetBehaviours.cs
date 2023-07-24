using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*"TargetBehaviours" script controls the behavior of target objects.
 It manages target interactions, scoring, timers, and teleportation
 of the targets within a specified area */
public class TargetBehaviours : MonoBehaviour
{
    private sorts Sort = new sorts(); // class to sort the scores array.
    private Vector3 foilVel; // variable representing the velocity of the "Foil" object
    private Vector3 prevFoilVel; //variable holding the previous position of the "Foil" object. It is used to calculate the velocity of the "Foil" object based on its position change over time.
    [SerializeField] private Transform foilPos;
    private bool timeFinished = true;
    public TextMeshProUGUI timerText; // allows updating and displaying the timer countdown during the game.
    public float GameTime; // sets the total duration of the game.
    private float seconds, minutes;
    private string secondsText = "00";
    public float disappearTimer; //It sets the duration for targets to reappear after being hit.
    public Vector3 origin;
    public float spawnRadius;
    public bool targetHit; //c bool variable indicating whether the target has been hit by the "Foil" object.
    private int score;
    private int[] scores = new int[4];
    public TextMeshProUGUI ScoreText, SavesText1, SavesText2, SavesText3; //he UI text elements for displaying scores. They allow updating and displaying the player's score and the top three scores.
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

/*"OnTriggerEnter" detects if the "Foil" object enters the trigger collider of the targe*/
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

    /*"OnTriggerStay" is called while a collider is inside a trigger.
     It helps manage the target interactions if the "Foil" object remains inside the trigger collider.*/
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Foil")
        {
            targetHit = false;
        }
    }


/*"Update" script updates the score, timer, and "Foil" velocity.*/
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


    /*"GameTimer" function is a coroutine that runs the game timer countdown.
        It handles the game timer logic and updates the timer UI.*/
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
/*"SortScore" function sorts the player's score and updates the top three scores.*/
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


    /*"TeleportTarget" function teleports the target to a random position within a
    specified radius around a given point.
        Parameters:
            O: The origin point for teleportation.
            radius: The maximum distance from the origin where the target can be teleported.*/
    private void TeleportTarget(Vector3 PO, float radius)
    {
        if (!timeFinished)
        {
            transform.position = PO + UnityEngine.Random.insideUnitSphere * radius;
        } 
    }

    /*"TargetTimer" manages the timer for target teleportation after a specified time, allowing
     the target to reappear after being hit.*/
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
