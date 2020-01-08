using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class SatListController : MonoBehaviour
{
    string[] satellites;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // we will search the Assets/Satellites directory to find
        // the available satellite types to add them to the list
        satellites = AssetDatabase.FindAssets("sat_", new string[] {"Assets/Resources/Satellites"});
        foreach (string satellite in satellites)
        {
            string path = AssetDatabase.GUIDToAssetPath(satellite).Substring(17);
            GameObject tmpSat = Resources.Load<GameObject>(path.Remove(path.Length - 7));
            Debug.Log(path.Remove(path.Length - 7));
            Debug.Log(tmpSat.GetComponent<SatelliteController>().sat_name);
        }
    }
}
