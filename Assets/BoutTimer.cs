using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoutTimer : MonoBehaviour
{
    public float BoutTime;
    private int seconds;
    private float minutes;
    [SerializeField] private TextMeshProUGUI time;
    
    // Update is called once per frame
    void Update()
    {
        time.text = minutes + ":" + seconds;
    }

    private IEnumerator TimerCount(float time)
    {
        while (time > 0) {
            time -= Time.deltaTime;
            seconds = Mathf.FloorToInt(time % 60);
            minutes = Mathf.FloorToInt(time / 60);
            yield return new WaitForEndOfFrame();
        }
    }
}
