using System.Collections;
using UnityEngine;

/*"SceneFade" script is responsible for fading the scene in and out 
using a specified color and duration. */
public class SceneFade : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColour;
    private Renderer rend;
    public GameObject screen;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        if (fadeOnStart)
        {
            FadeIn();
        }
        Debug.Log("why");
    }

    /*"FadeIn" function is used to initiate the fade-in effect.*/
    public void FadeIn()
    {
        Fade(1, 0, true);  /*It calls the Fade() function with alphaIn set to 1 (fully opaque) and alphaOut set to 0 (fully transparent) to start the fade-in.*/
    }

    /*"FadeOut" function is used to initiate the fade-out effect.*/
    public void FadeOut()
    {
        Fade(0, 1, false);  /*It calls the Fade() function with alphaIn set to 0 (fully transparent) and alphaOut set to 1 (fully opaque) to start the fade-out.*/  
    }


    /*"Fade" function is a helper function to initiate the fade effect.
        Parameters:
            alphaIn: The target alpha value for the fade-in effect (0 to 1).
            alphaOut: The target alpha value for the fade-out effect (0 to 1).
            inOrOut: A boolean flag to indicate whether it is a fade-in or fade-out (true for fade-in, false for fade-out).*/
    private void Fade(float alphaIn, float alphaOut, bool inOrOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut, inOrOut));
    }


    /*"FadeRoutine" Coroutine performs the actual fading effect over time.
        Parameters:
            alphaIn: The target alpha value for the fade-in effect (0 to 1).
            alphaOut: The target alpha value for the fade-out effect (0 to 1).
            inOrOut: A boolean flag to indicate whether it is a fade-in or fade-out (true for fade-in, false for fade-out).*/
    public IEnumerator FadeRoutine(float alphaIn, float alphaOut, bool inOrOut)
    {
        if (!inOrOut)
        {
            screen.transform.localScale = new Vector3(1, 1, 0);
        }
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColour = fadeColour;
            newColour.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            rend.material.SetColor("_Color", newColour);

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColour2 = fadeColour;
        newColour2.a = alphaOut;
        rend.material.SetColor("_Color", newColour2);

        if (inOrOut)
        {
            screen.transform.localScale = Vector3.zero;
        }
    }
}
