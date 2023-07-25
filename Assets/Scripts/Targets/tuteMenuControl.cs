using UnityEngine;

/* "tuteMenuControl" script controls a tutorial menu with multiple pages, 
allowing users to navigate between different tutorial content.*/
public class tuteMenuControl : MonoBehaviour
{
    public GameObject pg1, pg2, pg3, tuteMenu;
    public GameObject target;

    public void ChangePage(int page)
    {
        pg1.SetActive(page == 1);
        pg2.SetActive(page == 2);
        pg3.SetActive(page == 3);
        tuteMenu.SetActive(page != 4);
        if (page == 4)
        {
            target.SetActive(true);
        }
    }
}
