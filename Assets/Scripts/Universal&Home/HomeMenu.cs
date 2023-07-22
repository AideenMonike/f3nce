using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class HomeMenu : MonoBehaviour
{
    public GameObject homePage, settingsPage;
    public SceneTransitionManager sceneFade;
    public AudioMixer mixer;
    public Slider flex, maVol, muVol, fVol;

    void Update()
    {
        SetAudio();
        SetSliderValues();
    }
    private void SetAudio()
    {
        mixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("Master Volume"));
        mixer.SetFloat("FXVol", PlayerPrefs.GetFloat("FX Volume"));
        mixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("Music Volume"));
    }
    private void SetSliderValues()
    {
        flex.value = PlayerPrefs.GetFloat("FlexLevel");
        maVol.value = PlayerPrefs.GetFloat("Master Volume");
        muVol.value = PlayerPrefs.GetFloat("Music Volume");
        fVol.value = PlayerPrefs.GetFloat("FX Volume");
    }
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
        Application.Quit();
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
    public void BackToMenu()
    {
        settingsPage.SetActive(false);
        homePage.SetActive(true);
    }

    public void MasterVol(float num)
    {
        PlayerPrefs.SetFloat("Master Volume", num);
    }

    public void MusicVol(float num)
    {
        PlayerPrefs.SetFloat("Music Volume", num);
    }
    public void FXVol(float num)
    {
        PlayerPrefs.SetFloat("FX Volume", num);
    }
    public void Flex(float num)
    {
        PlayerPrefs.SetFloat("FlexLevel", num);
    }
}
