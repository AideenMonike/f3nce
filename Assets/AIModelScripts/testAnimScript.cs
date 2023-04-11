using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAnimScript : MonoBehaviour
{
    public GameObject model;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(TranslateModel());
    }
    private IEnumerator TranslateModel()
    {
        while (true){
            model.transform.position = model.transform.position - new Vector3(0.0005f, 0, 0);
            yield return new WaitForSeconds(5);
        }
        
    }
}
