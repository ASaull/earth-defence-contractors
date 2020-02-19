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
    const float GMe = 398380f;

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
    LineRenderer line;
    Vector3[] points;
    float radius;

    int anomaly = 0;


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
            orbiting = false;

            // We want to create the preview circle
            line = gameObject.AddComponent<LineRenderer>();
            line.useWorldSpace = false;
            line.startWidth = 0.02f;
            line.endWidth = 0.02f;
            line.positionCount = 361;

            points = new Vector3[361];

            DrawOrbit();
        }
    }

    void DrawOrbit()
    {
        radius = 1 + (float)altitude / 2000f;
        for (int i = 0; i < 361; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / 360);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }
            
        line.SetPositions(points);
    }

    // This function is called when the player is previewing the creation of this satellite.
    // The satellite will have been instantiated and will 
    void Preview()
    {

    }

    // This function is called when the player purchases this satellite.
    public void Deploy()
    {
        Debug.Log("deploying");
        orbiting = true;
    }

    // This function is called by the SatListController when
    // this satellite's inclination is updated
    public void ChangeInclination(int new_inc)
    {

    }

    // This function is called by the SatListController when
    // the satellite's longitude is updated
    public void ChangeLongitude(int new_long)
    {

    }
    
    // This function is called by the SatListControler when
    // the satellite's starting anomaly is updated
    public void ChangeAnomaly(int new_anom)
    {
        anomaly = new_anom;
        gameObject.transform.Rotate(new Vector3(0f, anomaly - gameObject.transform.eulerAngles.y, 0f));
    }

    // This function is called by the SatListController when
    // the satellite's Altitude is updated
    public void ChangeAltitude(int new_alt)
    {
        altitude = new_alt;
        satellite.transform.localPosition = new Vector3(1 + ((float)altitude / 2000f), 0f, 0f);
        DrawOrbit();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (orbiting)
        {
            float old = angular_velocity;
            angular_velocity = Mathf.Sqrt(GMe / Mathf.Pow(altitude, 2));

            gameObject.transform.Rotate(new Vector3(0f, angular_velocity, 0f));
        }
    }
}
