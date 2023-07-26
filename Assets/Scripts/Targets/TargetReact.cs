using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UIElements;
using UnityEngine;
using TMPro;
using Rand = UnityEngine.Random;

public class TargetReact : MonoBehaviour
{
    // Class for sorting highest scores. Contains a bubble sort
    sorts bsort = new sorts();
    public GameObject target, missZone, foil;
    // Point of Origin that the target teleports around and returns to
    private Vector3 origin, targetSize, defTargetSize, missZoneSize;
    [Range(1, 12)] public int targetTries;
    private float[] timeScores;
    public TextMeshProUGUI Score, AvgReactTime, Misses;
    public TextMeshProUGUI[] targetLabels, reactScore;
    [SerializeField]private float timeBetweenAppearance, timeToGo;
    private bool gameStarted = false;
    public bool targetHit = false, missWindow = false;
    private int misses;
    public bool foilReset;

    public GameObject returnLine/* , completeScreen */;

    // Start is called before the first frame update
    // Sets origin of the target
    void Start()
    {
        origin = target.transform.position;
        defTargetSize = new Vector3 (1f, 0.1f, 1f);
        
        targetSize = defTargetSize * 0.5f;

        misses = 0;
    }

    // Fixed update updates every frame when the physics engine updates.
    // Stops all coroutines when the game isn't running and updates the scoreboard
    void FixedUpdate()
    {
        UpdateTargetScoreboard();
    }

    public void ChangeTargetSize(float size)
    {
        targetSize = defTargetSize * Mathf.Clamp(size, 0.01f, 1f);
        target.transform.localScale = targetSize;
        missZone.transform.localScale = defTargetSize / size;
        Debug.Log(targetSize);  
    }

    public void ChangeTargetsRound(int num)
    {
        targetTries = num + 1;
    }

    // Updates the target board to contain the specified amount of targets to hit in one round
    private void UpdateTargetScoreboard()
    {
        if (!gameStarted)
        {
            float[] newSize = new float[targetTries];
            timeScores = newSize;
            for (int i = 0; i < 12; i++)
            {
                if (i < targetTries)
                {
                    targetLabels[i].text = "Target " + (i + 1);
                }
                if (i >= targetTries)
                {
                    reactScore[i].text = "---";
                    targetLabels[i].text = "---";
                }
            }
            Misses.text = "Misses: " + misses;
        }
        
    }

    // OnTrigger detects when a trigger collider collides with the target
    

    // Method to process collisions. Moreso for modularisation and an entrypoint for if a CollisionEnter method is added.
    public void WhenHit(Collider col)
    {
        if (!targetHit)
        {
            if (!gameStarted)
            {
                gameStarted = true;
                ResetVariables(timeToGo, timeBetweenAppearance);
            }
            else
            {
                targetHit = true;
            }
        }
    }

    public void MissTarget(Collider col)
    {
        if (gameStarted && !targetHit && missWindow)
        {
            misses++;    
        }
    }

    public void ResetFoil(Collider col)
    {
        foilReset = false;
    }

    public void UnResetFoil(Collider col)
    {
        foilReset = true;
    }

    // Creates the timer between targets teleporting randomly and triggers the game
    private void ResetVariables(float timeTeleport, float timeBetween)
    {
        misses = 0;
        StartCoroutine(ReactTimer(timeToGo, timeBetweenAppearance));
    }
    
    // Coroutine method that executes the gamemode
    private IEnumerator ReactTimer(float timeTeleport, float timeBetween)
    {
        int i = 0;
        while (i < targetTries)
        {
            float countdown = timeTeleport;
            float timer = Rand.Range(0.5f, timeBetween);
            target.transform.localScale = Vector3.zero;
            
            do
            {
                if (foilReset)
                {
                    returnLine.SetActive(true);
                }  
                yield return new WaitForSeconds(timer);
            } while (foilReset);
            returnLine.SetActive(false);

            target.transform.localScale = targetSize;
            RandomTeleportObject(target, origin);
            missWindow = true;

            float timeToHit = 0;
            targetHit = false;

            while (!targetHit && countdown > 0)
            {
                timeToHit += Time.deltaTime;
                countdown -= Time.deltaTime;

                string floatProcess = timeToHit.ToString();
                string reactTime = "";

                for (int j = 0; j < floatProcess.Length; j++)
                {
                    if (j <= 4)
                    {
                        reactTime = reactTime + floatProcess[j];
                    }
                }

                reactScore[i].text = reactTime + "s";

                if (countdown <= 0)
                {
                    reactScore[i].text = "NA";
                }
                yield return null;
            }

            timeScores[i] = timeToHit;
            if (i > 0)
            { 
                Debug.Log($"at position {i-1}, timeScores is {timeScores[i-1]}");
            }
            Debug.Log($"at position {i}, timeScores is {timeScores[i]}");
            missWindow = false;
            i++;
        }
        //completeScreen.SetActive(true);
        target.transform.localScale = Vector3.zero;
        for (int j = 0; j < timeScores.Length; j++)
        {   
            Debug.Log($"pos {j}:{timeScores[j]}");
        }

        float slideVal = targetSize.x / defTargetSize.x;
        Score.text = sorts.ScoreCalc(timeScores, slideVal, misses);

        AvgReactTime.text = sorts.AverageCalc(timeScores).ToString();
        yield return new WaitForSeconds(5);

        target.transform.position = origin;
        target.transform.localScale = targetSize;
        gameStarted = false;
        targetHit = false;
    }

    // Method to teleport object and ensures the target doesn't teleport into the player or their foil.
    private void RandomTeleportObject(GameObject obj, Vector3 origin)
    {
        obj.transform.position = origin + (0.3f * Rand.insideUnitSphere);
    }
}
