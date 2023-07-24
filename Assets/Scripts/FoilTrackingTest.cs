using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* "FoilTrackingTest"  simulates tracking the movement of a foil using a
 Rigidbody component. The script calculates the velocity of the foil and 
 applies it to the attached Rigidbody to move the Foil accordingly*/
public class FoilTrackingTest : MonoBehaviour
{
    private Rigidbody rb;
    public Transform foil;
    public Collider foilBox;
    public Transform handBox;
    Vector3 foilVel;
    Vector3 previousFoilPos;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        previousFoilPos = foil.position;
        //StartCoroutine(AttachToHand());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foilVel = (foil.position - previousFoilPos) / Time.deltaTime; //using the physics formula v = u + at
        previousFoilPos = foil.position;
        /*
        Debug.Log(foilVel);
        Debug.Log("---");
        */
        Debug.Log(rb.velocity);
        
        rb.velocity = foilVel; 
    }
    private IEnumerator AttachToHand()
    {
        while (true)
        {
           foil.position = handBox.position - new Vector3(1, 0 , 1);
            yield return null; 
        }
    }
}
