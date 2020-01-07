using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SatListController : MonoBehaviour
{
    string[] satellites;

    // Start is called before the first frame update
    void Start()
    {
        // we will search the Assets/Satellites directory to find
        // the available satellite types to add them to the list
        satellites = AssetDatabase.FindAssets("sat_*", new string[] {"Assets/Satellites"});
        foreach (string satellite in satellites)
        {
            string path = AssetDatabase.GUIDToAssetPath(satellite);
            SatelliteController tmpSat = (SatelliteController)AssetDatabase.LoadAssetAtPath(path, typeof(SatelliteController));
            Debug.Log(tmpSat.sat_name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
