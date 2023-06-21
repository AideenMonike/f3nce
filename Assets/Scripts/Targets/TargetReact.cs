using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Rand = UnityEngine.Random;

public class TargetReact : MonoBehaviour
{
    // Class for sorting highest scores. Contains a bubble sort
    sorts bsort = new sorts();
    public GameObject target;
    // Point of Origin that the target teleports around and returns to
    private Vector3 origin;
    [Range(1, 12)] public int targetTries;
    public TextMeshProUGUI Score, AvgReactTime, Misses;
    public TextMeshProUGUI[] targetLabels, reactScore;
    [SerializeField]private float timeBetweenAppearance, timeToGo;
    private bool gameStarted = false;
    public bool targetHit = false;
    private Collider[] objectCol;
    private int misses;
    public bool foilReset;

    // Start is called before the first frame update
    // Sets origin of the target
    void Start()
    {
        origin = target.transform.position;
        objectCol = GetComponents<Collider>();

        misses = 0;
        foreach (Collider col in objectCol)
        {
            Debug.Log(col.GetType());
        }
    }

    // Fixed update updates every frame when the physics engine updates.
    // Stops all coroutines when the game isn't running and updates the scoreboard
    void FixedUpdate()
    {
        UpdateTargetScoreboard();
    }

    // Updates the target board to contain the specified amount of targets to hit in one round
    private void UpdateTargetScoreboard()
    {
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
        if (gameStarted && !targetHit)
        {
            misses++;    
        }
    }

    public void ResetFoil(Collider col)
    {
        foilReset = false;
        Debug.Log($"certified {foilReset} moment");
    }

    public void UnResetFoil(Collider col)
    {
        foilReset = true;
        Debug.Log($"The voices scream {foilReset}");
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
        Debug.Log($"I'm losing my mind");
        int i = 0;
        while (i < targetTries)
        {
            float countdown = timeTeleport;
            float timer = Rand.Range(0.5f, timeBetween);
            target.transform.localScale = Vector3.zero;
            
            yield return new WaitForSeconds(timer);
            
            Debug.Log($"Foil reset is {foilReset}");
            while (foilReset)
            {
                yield return new WaitForSecondsRealtime(3);
            }

            target.transform.localScale = new Vector3 (0.5f, 0.05f, 0.5f);
            RandomTeleportObject(target, origin);

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
            Debug.Log("ëyes");
            i++;
        }
        target.transform.position = origin;
        gameStarted = false;
        targetHit = false;
    }

    // Method to teleport object and ensures the target doesn't teleport into the player or their foil.
    private void RandomTeleportObject(GameObject obj, Vector3 origin)
    {
        // need a way to determine how to not teleport onto the foil
        // also maybe a way to restrict domain
        obj.transform.position = origin + (1.5f * Rand.insideUnitSphere);
    }
}
