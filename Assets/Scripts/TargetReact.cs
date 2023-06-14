using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Rand = UnityEngine.Random;

// MAKE A TARGET SYSTEM BASED ON NUMBER OF TARGETS AND REACTION OVER AMOUNT HIT IN THIS TIME
public class TargetReact : MonoBehaviour
{
    sorts bsort = new sorts();
    public GameObject target;
    [Range(1, 10)] public int targetTries;
    public TextMeshProUGUI Timer, AvgReactTime;
    private TextMeshProUGUI[] reactScore;
    private float timeBetweenAppearance;
    private bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        reactScore = new TextMeshProUGUI[targetTries];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
