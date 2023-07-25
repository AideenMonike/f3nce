using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/* "HomeMenu"  script controls the functionality of the home menu, including scene
 transitions, audio settings, and slider values. */

public class HomeMenu : MonoBehaviour
{
    public GameObject homePage, settingsPage;
    public SceneTransitionManager sceneFade;
    public AudioMixer mixer;
    public Slider flex, maVol, muVol, fVol;


/*"Update"" function is called every frame and updates the audio 
settings and slider values accordingly.*/
    void Update()
    {
        SetAudio();
        SetSliderValues();
    }

    /*"SetAudio" This function sets the audio levels (volume) in the AudioMixer 
    based on player preferences stored in PlayerPrefs.*/
    private void SetAudio()
    {
        mixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("Master Volume"));
        mixer.SetFloat("FXVol", PlayerPrefs.GetFloat("FX Volume"));
        mixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("Music Volume"));
    }

    /*"SetSliderValues"" function sets the slider values of the UI elements 
    (flex, maVol, muVol, fVol) based on player preferences stored in PlayerPrefs.*/
    private void SetSliderValues()
    {
        flex.value = PlayerPrefs.GetFloat("FlexLevel");
        maVol.value = PlayerPrefs.GetFloat("Master Volume");
        muVol.value = PlayerPrefs.GetFloat("Music Volume");
        fVol.value = PlayerPrefs.GetFloat("FX Volume");
    }

    /*"PlayTargets" function is called when the "Play Targets" button is pressed. 
     It initiates a scene transition to the Targets scene.*/
    public void PlayTargets()
    {
        sceneFade.GoToScene(1);
    }

    /*"FreeSandbox" function is called when the "Free Sandbox" button is pressed. 
    It initiates a scene transition to the Free Sandbox scene.*/
    public void FreeSandbox()
    {
        sceneFade.GoToScene(2);
    }

    /*"Exit" function is called when the "Exit" button is pressed. 
    It terminates the application and closes the game.*/
    public void Exit()
    {
        Application.Quit();
    }

    /*"SettingsOpen" function is called when the "Settings" button is pressed.
     It opens the settings page and hides the home page.*/
    public void SettingsOpen()
    {
        settingsPage.SetActive(true);
        homePage.SetActive(false);
    }

    /*"SwapHands" function is called when the "Swap Hands" button is pressed. 
     It toggles the player's preference for the main hand between left and right.*/
    public void SwapHands()
    {
        int hand = PlayerPrefs.GetInt("Main Hand");
        hand = Mathf.Abs(PlayerPrefs.GetInt("Main Hand") - 1);
        PlayerPrefs.SetInt("Main Hand", hand);
        Debug.Log(PlayerPrefs.GetInt("Main Hand"));
    }

    /*"BackToMenu" function is called when the "Back" button is pressed on the settings page. 
    It closes the settings page and returns to the home page.*/
    public void BackToMenu()
    {
        settingsPage.SetActive(false);
        homePage.SetActive(true);
    }

    /*"MasterVol" function is called when the master volume slider value changes. 
     It sets the master volume in PlayerPrefs based on the slider value.*/
    public void MasterVol(float num)
    {
        PlayerPrefs.SetFloat("Master Volume", num);
    }

    /*"MusicVol" function is called when the music volume slider value changes. 
    It sets the music volume in PlayerPrefs based on the slider value.*/
    public void MusicVol(float num)
    {
        PlayerPrefs.SetFloat("Music Volume", num);
    }

    /*"FXVol" unction is called when the sound effects volume slider value changes. 
    It sets the sound effects volume in PlayerPrefs based on the slider value.*/
    public void FXVol(float num)
    {
        PlayerPrefs.SetFloat("FX Volume", num);
    }

    /*"Flex" function is called when the flex slider value changes. 
    It sets the flex level in PlayerPrefs based on the slider value.*/
    public void Flex(float num)
    {
        PlayerPrefs.SetFloat("FlexLevel", num);
    }
}
