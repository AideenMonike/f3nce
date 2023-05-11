using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class CRTShadeApply : MonoBehaviour
{
    public Shader shader;
    public float bend = 4f;
    public float scanlineSize1 = 200;
    public float scanlineSpeed1 = -10;
    public float scanlineSize2 = 20;
    public float scanlineSpeed2 = -3;
    public float scanlineAmount = 0.05f;
    public float vignetteSize = 1.9f;
    public float vignetteSmoothness = 0.6f;
    public float vignetteEdgeRound = 8f;
    public float noiseSize = 75f;
    public float noiseAmount = 0.05f;

    public Vector2 redOffset = new Vector2 (0, -0.01f);
    public Vector2 greenOffset = Vector2.zero;
    public Vector2 blueOffset = new Vector2 (0, 0.01f);


    private Material mat;
    // Start is called before the first frame update
    void Awake()
    {
        mat = new Material(shader);
    }
    /* void OnRenderObject()
    {
        mat.SetFloat("u_time", Time.fixedTime);
        mat.SetFloat("u_bend", bend);
        mat.SetFloat("u_scanline_size1", scanlineSize1);
        mat.SetFloat("u_scanline_spd1", scanlineSpeed1);
        mat.SetFloat("u_scanline_size2", scanlineSize2);
        mat.SetFloat("u_scanline_spd2", scanlineSpeed2);
        mat.SetFloat("u_scanline_No", scanlineAmount);
        mat.SetFloat("u_vigSize", vignetteSize);
        mat.SetFloat("u_vigSmooth", vignetteSmoothness);
        mat.SetFloat("u_vigEdgeRnd", vignetteEdgeRound);
        mat.SetFloat("u_noiseSize", noiseSize);
        mat.SetFloat("u_noiseNo", noiseAmount);
        mat.SetVector("u_redOff", redOffset);
        mat.SetVector("u_blueOff", blueOffset);
        mat.SetVector("u_greenOff", greenOffset);
        //Graphics.Blit(src, dest, mat);
    } */

    // Update is called once per frame
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        //Debug.Log("hit");
        mat.SetFloat("u_time", Time.fixedTime);
        mat.SetFloat("u_bend", bend);
        mat.SetFloat("u_scanline_size1", scanlineSize1);
        mat.SetFloat("u_scanline_spd1", scanlineSpeed1);
        mat.SetFloat("u_scanline_size2", scanlineSize2);
        mat.SetFloat("u_scanline_spd2", scanlineSpeed2);
        mat.SetFloat("u_scanline_No", scanlineAmount);
        mat.SetFloat("u_vigSize", vignetteSize);
        mat.SetFloat("u_vigSmooth", vignetteSmoothness);
        mat.SetFloat("u_vigEdgeRnd", vignetteEdgeRound);
        mat.SetFloat("u_noiseSize", noiseSize);
        mat.SetFloat("u_noiseNo", noiseAmount);
        mat.SetVector("u_redOff", redOffset);
        mat.SetVector("u_blueOff", blueOffset);
        mat.SetVector("u_greenOff", greenOffset);
        Graphics.Blit(src, dest, mat);
    }
} 
