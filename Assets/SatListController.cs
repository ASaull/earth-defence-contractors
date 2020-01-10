using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SatListController : MonoBehaviour
{
    public GameObject[] satellites;
    public GameObject base_button;
    public GameObject info_panel;

    private GameObject current_sat = null;
    private Text info_title;


    // Start is called before the first frame update
    void Start()
    {
        // we hide the info panel
        info_title = info_panel.transform.Find("TitlePanel").Find("PanelTitle").gameObject.GetComponent<Text>();
        info_panel.SetActive(false);


        // we will search the Assets/Satellites directory to find
        // the available satellite types to add them to the list
        foreach (GameObject satellite in satellites)
        {
            //we now have to create a button to allow us to create a tmpsat
            GameObject tmp_button = (GameObject)Instantiate(base_button, transform);
            tmp_button.GetComponentInChildren<Text>().text = satellite.GetComponent<SatelliteController>().sat_name;
            tmp_button.GetComponent<Button>().onClick.AddListener(delegate{Preview(satellite);});
        }


    }

    // This function will update the info panel for the current satellite.
    void UpdatePanel()
    {
        info_title.text = current_sat.GetComponent<SatelliteController>().sat_name;
    }

    // This function will toggle the satellite of type sat to be created or destroyed in preview mode
    void Preview(GameObject sat)
    {
        if (current_sat == null)
        {
            current_sat = Instantiate(sat, new Vector3(0, 0, 0), Quaternion.identity);
            UpdatePanel();
            info_panel.SetActive(true);
        }
        else if (current_sat.GetComponent<SatelliteController>().sat_name == sat.GetComponent<SatelliteController>().sat_name)
        {
            Destroy(current_sat);
            info_panel.SetActive(false);
        }
        else
        {
            Destroy(current_sat);
            current_sat = Instantiate(sat, new Vector3(0, 0, 0), Quaternion.identity);
            UpdatePanel();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
