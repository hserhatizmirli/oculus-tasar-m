using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Hmdınfomanager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("is device active." + XRSettings.isDeviceActive);
        Debug.Log("is device name: " + XRSettings.loadedDeviceName);

        if (!XRSettings.isDeviceActive)
        {
            Debug.Log("No headset plugged");
        }
        else if (XRSettings.isDeviceActive && XRSettings.loadedDeviceName == "mock mhd" || XRSettings.loadedDeviceName == "MockHMDDisplay")
        {
            Debug.Log("using mock mhd");
        }
        else
        {
            Debug.Log("we have a headset" + XRSettings.loadedDeviceName);
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
