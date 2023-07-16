﻿using System.Collections;
using UnityEngine;

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

    public void FadeIn()
    {
        Fade(1, 0, true);
    }

    public void FadeOut()
    {
        Fade(0, 1, false);        
    }

    private void Fade(float alphaIn, float alphaOut, bool inOrOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut, inOrOut));
    }

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
