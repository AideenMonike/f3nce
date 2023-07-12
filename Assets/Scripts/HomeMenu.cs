using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeMenu : MonoBehaviour
{
    public GameObject homePage, settingsPage;
    public SceneTransitionManager sceneFade;

    public void PlayTargets()
    {
        sceneFade.GoToScene(1);
    }
    public void FreeSandbox()
    {
        sceneFade.GoToScene(2);
    }
    public void Exit()
    {

    }
    public void SettingsOpen()
    {
        settingsPage.SetActive(true);
        homePage.SetActive(false);
    }

    public void SwapHands()
    {

    }
    public void CalibrateHeight()
    {

    }
    public void BackToMenu()
    {
        settingsPage.SetActive(false);
        homePage.SetActive(true);
    }

    public void MusicVol(float num)
    {

    }
    public void FXVol(float num)
    {

    }
    public void Flex(float num)
    {

    }
}
