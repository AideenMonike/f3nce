using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PersonalMenuControl : MonoBehaviour
{
    public InputActionProperty menuButton;
    public GameObject menu;
    public Animator anim;

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
