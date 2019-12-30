using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player_prefab;
    private List<GameObject> player_list;
    public List<Corporation> corp_list;
    private int base_health;
    private int round_number;
    //private int enemy_shield_percent
    //private int enemy_shield_pierce_percent
    public List<GameObject> enemy_types;
    private string game_state = "introduction";
    private int defence_budget;

    void calculate_budgets()
    {
        int total_performance = 0;
        foreach (Corporation corp in corp_list)
        {
            total_performance += corp.performance;
        }
        
        foreach (Corporation corp in corp_list)
        {
            corp.budget = (corp.performance / total_performance)
                          * defence_budget;
        }

    }

    void create_players(int n, List<string> names)
    {
        for (int i = 0; i < n; i++)
        {
            GameObject player = Instantiate(player_prefab, new Vector3(0, 0, 0),
                                            Quaternion.identity);
            Corporation corp = player.GetComponent<Corporation>();
            corp.name = names[i];
            corp_list.Add(corp);
            player_list.Add(player);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        var name_list = new List<string>() {"Hayabusa Heavy Industries",
                                  "Stark Industries"};
        create_players(2, name_list);
        //foreach (GameObject go in player_list)
        //{
        //   corp_list.Add(go.GetComponent<Corporation>());
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
