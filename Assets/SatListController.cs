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
    private SatelliteController current_sat_controller;
    private Text info_title;

    public InputField inclination_field;
    public Slider inclination_slider;
    private int old_inc;

    public InputField altitude_field;
    public Slider altitude_slider;
    private int old_alt;

    public InputField anomaly_field;
    public Slider anomaly_slider;
    private int old_anom;

    public InputField longitude_field;
    public Slider longitude_slider;
    private int old_long;

    private Slider[] sliders;


    // Start is called before the first frame update
    void Start()
    {
        // we hide the info panel
        info_title = info_panel.transform.Find("TitlePanel").Find("PanelTitle").gameObject.GetComponent<Text>();

        info_panel.SetActive(false);
        sliders = new Slider[] {inclination_slider, altitude_slider, anomaly_slider, longitude_slider};

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
        foreach (Slider slider in sliders)
        {
            slider.value = slider.minValue;
        } 
    }

    // This function will toggle the satellite of type sat to be created or destroyed in preview mode
    void Preview(GameObject sat)
    {
        if (current_sat == null) // We are creating a new Satellite
        {
            current_sat = Instantiate(sat, new Vector3(0, 0, 0), Quaternion.identity);
            current_sat_controller = current_sat.GetComponent<SatelliteController>();
            UpdatePanel();
            info_panel.SetActive(true);
        }
        else if (current_sat.GetComponent<SatelliteController>().sat_name == sat.GetComponent<SatelliteController>().sat_name) // We are destroying current Satellite
        {
            Destroy(current_sat);
            info_panel.SetActive(false);
        }
        else // We are creating a new Satellite and destroying the old one
        {
            Destroy(current_sat);
            current_sat = Instantiate(sat, new Vector3(0, 0, 0), Quaternion.identity);
            current_sat_controller = current_sat.GetComponent<SatelliteController>();
            UpdatePanel();
        }
    }

    // Update is called once per frame
    void Update()
    {
         // If there is a currently active satellite then we want
         // to listed to changes in the parameter sliders
         if (current_sat != null)
         {
             if (old_alt != altitude_slider.value)
             {
                 // Altitude was changed
                 current_sat_controller.ChangeAltitude((int)altitude_slider.value);
                 old_alt = (int)altitude_slider.value;
             }
         }
    }
}
