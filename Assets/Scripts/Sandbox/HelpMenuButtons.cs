using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenuButtons : MonoBehaviour
{
    public GameObject pg1, pg2, pg3;

    public void ToPageOne()
    {
        pg2.SetActive(false);
        pg3.SetActive(false);
        pg1.SetActive(true);
    }
    public void ToPageTwo()
    {
        pg1.SetActive(false);
        pg3.SetActive(false);
        pg2.SetActive(true);
    }
    public void ToPageThree()
    {
        pg1.SetActive(false);
        pg2.SetActive(false);
        pg3.SetActive(true);
    }
}
