using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapHands : MonoBehaviour
{
    public GameObject LFoil, RFoil;

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Main Hand") == 1)
        {
            Debug.Log("Yes");
            LFoil.SetActive(true);
            RFoil.SetActive(false);
        }
        else
        {
            LFoil.SetActive(false);
            RFoil.SetActive(true);
        }
    }
}
