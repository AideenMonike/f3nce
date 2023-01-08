using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoilBehaviour : MonoBehaviour
{
    public static FoilBehaviour Instance;

    public GameObject[] Bones;

    public Vector3 Centre;

    public SkinnedMeshRenderer SMRenderer;
    public Mesh FoilBakedMesh;

    Vector3 vel;

    private void Awake() 
    {
        Instance = this;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i = 0; i < Bones.Length; i++)
        {
            Centre += Bones[i].transform.position;
        }
        Centre = Centre / Bones.Length;

        transform.position = Centre;

        Centre = Vector3.zero;

        FoilBakeMeshToCollider();
    }

    void FoilBakeMeshToCollider()
    {
        SMRenderer.BakeMesh(FoilBakedMesh);
        gameObject.GetComponent<MeshCollider>().sharedMesh = FoilBakedMesh;
    }
}
