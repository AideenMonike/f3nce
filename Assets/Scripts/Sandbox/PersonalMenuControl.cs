using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/*"PersonalMenuControl"  script handles menu control, animation triggers, and 
scene reloading based on player input and menu interactions.*/
public class PersonalMenuControl : MonoBehaviour
{
    public InputActionProperty menuButton; //It allows the assignment of an InputAction from the Unity Input System to detect menu button presses.
    public GameObject menu; //It allows toggling the visibility of the menu in the scene.
    public Animator anim; //It allows controlling animations associated with fencer character movements or actions.

    // Update is called once per frame
    void Update()
    {
        if (menuButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene("Sandbox");
    }

    public void AdvThrust(bool arg)
    {
        Debug.Log($"why {arg}");
        anim.SetBool("Advance Thrust", arg);
        anim.SetBool("Idle", !arg);
    }

    public void Parry(bool arg)
    {
        anim.SetBool("Prime Parry", arg);
        anim.SetBool("Idle", !arg);
    }

    public void Riposte(bool arg)
    {
        anim.SetBool("Riposte", arg);
        anim.SetBool("Idle", !arg);
    }

    public void Lunge(bool arg)
    {
        anim.SetBool("The Lunge", arg);
        anim.SetBool("Idle", !arg);
    }
}
