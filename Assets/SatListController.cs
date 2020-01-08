using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SatListController : MonoBehaviour
{
    string[] satellites;
    public GameObject base_button;

    private GameObject current_sat = null;

    // Start is called before the first frame update
    void Start()
    {
        // we will search the Assets/Satellites directory to find
        // the available satellite types to add them to the list
        satellites = AssetDatabase.FindAssets("sat_", new string[] {"Assets/Resources/Satellites"});
        foreach (string satellite in satellites)
        {
            string path = AssetDatabase.GUIDToAssetPath(satellite).Substring(17);
            GameObject tmpSat = Resources.Load<GameObject>(path.Remove(path.Length - 7));
            //we now have to create a button to allow us to create a tmpsat
            GameObject tmp_button = (GameObject)Instantiate(base_button, transform);
            tmp_button.GetComponentInChildren<Text>().text = tmpSat.GetComponent<SatelliteController>().sat_name;
            tmp_button.GetComponent<Button>().onClick.AddListener(delegate{Preview(tmpSat);});
        }


    }

    // This function will toggle the satellite of type sat to be created or destroyed in preview mode
    void Preview(GameObject sat)
    {
        if (current_sat == null)
        {
            current_sat = Instantiate(sat, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            Destroy(current_sat);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
