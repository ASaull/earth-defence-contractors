using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase
{
    Preview = 0,
    Inactive = 1,
    Active = 2,
}

[System.Serializable]
public class SatelliteController : MonoBehaviour
{
    // constants
    const float GMe = 39838.0f;

    // Public variables
    public int altitude;
    public Phase current_phase = Phase.Preview;
    public Corporation owner;
    public GameObject satellite;
    public string sat_name = "Default. Did you specify a satellite type?";

    //Private  variables
    bool orbiting = false;
    float angular_velocity;
    int old_alt;


    // Start is called before the first frame update
    void Start()
    {
        // Start will be called when this object is instantiated
        // It will either be in Preview or Inactive phase.
        if (current_phase == Phase.Preview)
        {
            // In this case, the current user is previewing this satellite
            // We start it out with a base altitude of 400, and we preview it orbiting
            //altitude = 300;
            orbiting = true;
        }
    }

    // This function is called when the player is previewing the creation of this satellite.
    // The satellite will have been instantiated and will 
    void Preview()
    {

    }

    // This function is called when the player purchases this satellite.
    void Deploy()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (orbiting)
        {
            float old = angular_velocity;
            angular_velocity = Mathf.Sqrt(GMe / Mathf.Pow(altitude, 2));

            gameObject.transform.Rotate(new Vector3(0f, angular_velocity, 0f));
        }

        // We always adjust the distance
        if (altitude != old_alt)
        {
            // alt was changed, adjust distance where each .15 is 300km
            // starting at 1
            satellite.transform.position = new Vector3(1 + ((float)altitude / 2000f), 0f, 0f);
        }
        old_alt = altitude;
    }
}
