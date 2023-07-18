using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapHands : MonoBehaviour
{
    public GameObject LFoil, RFoil, LControl, RControl;

    // Update is called once per frame
    void Update()
    {
        LFoil.SetActive(PlayerPrefs.GetInt("Main Hand") == 1);
        RControl.SetActive(PlayerPrefs.GetInt("Main Hand") == 1);
        RFoil.SetActive(PlayerPrefs.GetInt("Main Hand") == 0);
        LControl.SetActive(PlayerPrefs.GetInt("Main Hand") == 0);
    }
}
