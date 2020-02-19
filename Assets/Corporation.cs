using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corporation : MonoBehaviour
{
    public int budget;
    private int funds;
    public int spent = 0;
    public int performance = 0;
    public List<GameObject> satellites;

    // Start is called before the first frame update
    void Start()
    {
        satellites = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSatellite(GameObject sat)
    {
        satellites.Add(sat);
    }
}
