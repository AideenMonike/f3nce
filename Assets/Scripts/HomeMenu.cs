using UnityEngine;
using UnityEngine.Audio;

public class HomeMenu : MonoBehaviour
{
    public GameObject homePage, settingsPage;
    public SceneTransitionManager sceneFade;
    public AudioMixer mixer;

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
        int hand = PlayerPrefs.GetInt("Main Hand");
        hand = Mathf.Abs(PlayerPrefs.GetInt("Main Hand") - 1);
        PlayerPrefs.SetInt("Main Hand", hand);
        Debug.Log(PlayerPrefs.GetInt("Main Hand"));
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
        PlayerPrefs.SetFloat("Music Volume", num);
        mixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("Music Volume"));
    }
    public void FXVol(float num)
    {
        PlayerPrefs.SetFloat("FX Volume", num);
        mixer.SetFloat("FXVol", PlayerPrefs.GetFloat("FX Volume"));
    }
    public void Flex(float num)
    {
        PlayerPrefs.SetFloat("FlexLevel", num);
    }
}
