using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using Rand = UnityEngine.Random;

public class TargetReact : MonoBehaviour
{
    public GameObject target, missZone;
    // Point of Origin that the target teleports around and returns to
    private Vector3 origin, targetSize, defTargetSize;
    [Range(1, 12)] public int targetTries;
    private float[] timeScores;
    public TextMeshProUGUI Score, AvgReactTime, Misses;
    public TextMeshProUGUI[] targetLabels, reactScore;
    [SerializeField]private float timeBetweenAppearance, timeToGo;
    private bool gameStarted = false;
    public bool targetHit = false, missWindow = false;
    private int misses;
    public bool foilReset;

    public GameObject returnLine;
    public AudioSource hitSound;
    public GameObject menu;

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
        menu.SetActive(!gameStarted);
        UpdateTargetScoreboard();
    }

    public void ChangeTargetSize(float size)
    {
        if (!gameStarted)
        {
            targetSize = defTargetSize * Mathf.Clamp(size, 0.01f, 1f);
            target.transform.localScale = targetSize;
            missZone.transform.localScale = defTargetSize / size;
            Debug.Log(targetSize);  
        }
        
    }

    public void ChangeTargetsRound(int num)
    {
        if (!gameStarted)
        {
            targetTries = num + 1;
        }
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
                hitSound.PlayOneShot(hitSound.clip);
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
            
            float timeBeforeTel = timer;
            while (timeBeforeTel > 0)
            {
                //Debug.Log("passed");
                timeBeforeTel = TimeReset(timer, timeBeforeTel);
                timeBeforeTel -= Time.deltaTime;
                yield return null;
            }
            
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
            missWindow = false;
            i++;
        }
        //completeScreen.SetActive(true);
        target.transform.localScale = Vector3.zero;

        float slideVal = targetSize.x / defTargetSize.x;
        Score.text = "Score: " + sorts.ScoreCalc(timeScores, slideVal, misses);

        string rawAvgTime = sorts.AverageCalc(timeScores).ToString();
        string avgTime = "";
        for (int j = 0; j <= 3; j++)
        {
            avgTime = avgTime + rawAvgTime[j];
        }
        AvgReactTime.text = "Avg Time: " + avgTime;
        yield return new WaitForSeconds(5);

        target.transform.position = origin;
        target.transform.localScale = targetSize;
        gameStarted = false;
        targetHit = false;
    }

    // Method to teleport object 
    private void RandomTeleportObject(GameObject obj, Vector3 origin)
    {
        obj.transform.position = origin + (0.3f * Rand.insideUnitSphere);
    }

    private float TimeReset(float originalTime, float currentTime)
    {
        if (foilReset)
        {
            returnLine.SetActive(true);
            return originalTime;
        }
        else
        {
            returnLine.SetActive(false);
            return currentTime;
        }
    }
}
