using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*"SwapHands" is responsible for swapping between left and right hand controls 
and foils based on the player's preference or choice. It reads a value stored
 in the PlayerPrefs system and activates or deactivates objects 
 (LFoil, RFoil, LControl, and RControl) accordingly*/
public class SwapHands : MonoBehaviour
{
    public GameObject LFoil, RFoil, LControl, RControl;

    // Update is called once per frame
    void Update()
    {
         /*If the value stored in PlayerPrefs for the "Main Hand" key is 1 (indicating 
         the player's main hand is left), LFoil will be set active; otherwise, 
         it will be deactivated*/
        LFoil.SetActive(PlayerPrefs.GetInt("Main Hand") == 1);

        /*If the value stored in PlayerPrefs for the "Main Hand" key is 1 (indicating the 
        player's main hand is left), RControl will be set active; otherwise, it will be deactivated*/
        RControl.SetActive(PlayerPrefs.GetInt("Main Hand") == 1);
      
        /*If the value stored in PlayerPrefs for the "Main Hand" key is 0 (indicating the player's 
        main hand is right), RFoil will be set active; otherwise, it will be deactivated.*/
        RFoil.SetActive(PlayerPrefs.GetInt("Main Hand") == 0);

        /*If the value stored in PlayerPrefs for the "Main Hand" key is 0 (indicating the 
        player's main hand is right), LControl will be set active; otherwise, it 
        will be deactivated.*/
        LControl.SetActive(PlayerPrefs.GetInt("Main Hand") == 0);
    }
}
