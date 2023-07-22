using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
