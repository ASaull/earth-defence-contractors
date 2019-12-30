using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;

public class MainMenuController : MonoBehaviour
{
    //Public variables
    public Canvas main_menu_canvas;
    public Canvas lobby_canvas;
    public Text[] player_labels;

    string name;

    // Start is called before the first frame update
    void Start()
    {
        // We start by hiding the lobby canvas, this will be shown later
        lobby_canvas.enabled = false;

        if(SteamManager.Initialized)
        {
            name = SteamFriends.GetPersonaName();
            player_labels[0].text = name;
            SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, 4);
        }
        
    }

    public void OnPlayButtonClick()
    {
        lobby_canvas.enabled = true;
        main_menu_canvas.enabled = false;
    }

    public void OnBackButtonClick()
    {
        lobby_canvas.enabled = false;
        main_menu_canvas.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
