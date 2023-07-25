using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/*"HandPresence" script is used to get information about 
 connected input devices, specifically XR (VR/AR) devices. */
public class HandPresence : MonoBehaviour
{
    //i have committed :)
    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
