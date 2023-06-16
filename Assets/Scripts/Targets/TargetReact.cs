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
    public TextMeshProUGUI Score, AvgReactTime;
    public TextMeshProUGUI[] targetLabels, reactScore;
    [SerializeField]private float timeBetweenAppearance, timeToGo;
    private bool gameStarted = false;
    public bool targetHit = false;

    // Start is called before the first frame update
    // Sets origin of the target
    void Start()
    {
        origin = target.transform.position;
    }

    // Fixed update updates every frame when the physics engine updates.
    // Stops all coroutines when the game isn't running and updates the scoreboard
    void FixedUpdate()
    {
        if (!gameStarted)
        {
            StopAllCoroutines();
        }
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
    }

    // OnTrigger detects when a trigger collider collides with the target
    void OnTriggerEnter(Collider other)
    {
        ProcessCollision(other.gameObject);
    }

    // Method to process collisions. Moreso for modularisation and an entrypoint for if a CollisionEnter method is added.
    private void ProcessCollision(GameObject col)
    {
        if (col.CompareTag("Foil") && !targetHit)
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

    // Creates the timer between targets teleporting randomly and triggers the game
    private void ResetVariables(float timeTeleport, float timeBetween)
    {
        float timer = Rand.Range(0.5f, timeBetween);
        
        StartCoroutine(ReactTimer(timer));
    }
    
    // Coroutine method that executes the gamemode
    private IEnumerator ReactTimer(float time)
    {
        int i = 0;
        while (i < targetTries)
        {
            target.transform.localScale = Vector3.zero;
            yield return new WaitForSeconds(time);
            
            target.transform.localScale = new Vector3 (0.5f, 0.05f, 0.5f);
            RandomTeleportObject(target, origin);

            float timeToHit = 0;
            targetHit = false;

            while (!targetHit)
            {
                timeToHit += Time.deltaTime;
                yield return null;
            }
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
            i++;
        }
        target.transform.position = origin;
        gameStarted = false;
    }

    // Method to teleport object and ensures the target doesn't teleport into the player or their foil.
    private void RandomTeleportObject(GameObject obj, Vector3 origin)
    {
        // need a way to determine how to not teleport onto the foil
        // also maybe a way to restrict domain
        obj.transform.position = origin + (1.5f * Rand.insideUnitSphere);
    }
}
