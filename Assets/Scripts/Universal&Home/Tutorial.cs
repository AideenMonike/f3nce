using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*"Tutorial" function is used to load the specified tutorial page and 
 control the visibility of tutorial and home game objects.
    Parameters:
        page: The page number of the tutorial to load. It is an integer value representing the desired tutorial page.*/
public class Tutorial : MonoBehaviour
{
    public GameObject tut, pg1, home;

    public void LoadPage(int page)
    {
        pg1.SetActive(page == 1);
        if (page == 4)
        {
            home.SetActive(true);
            tut.SetActive(false);
        }
    }
}
