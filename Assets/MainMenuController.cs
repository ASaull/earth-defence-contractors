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
    int players_in_lobby;

    protected Callback<LobbyCreated_t> Callback_lobbyCreated;
    protected Callback<LobbyMatchList_t> Callback_lobbyList;
    protected Callback<LobbyEnter_t> Callback_lobbyEnter;
    protected Callback<LobbyDataUpdate_t> Callback_lobbyInfo;
    protected Callback<GameLobbyJoinRequested_t> Callback_joinRequested;
    protected Callback<LobbyChatUpdate_t> Callback_chatUpdate;

    ulong current_lobbyID;
    Steamworks.CSteamID lobbyID;
    List<CSteamID> lobbyIDS;

    // Use this for initialization
    void Start () {

        lobby_canvas.enabled = false;

        lobbyIDS = new List<CSteamID>();

        if (SteamAPI.Init())
        {
            Debug.Log("Steam API init -- SUCCESS!");
            name = SteamFriends.GetPersonaName();
            //player_labels[0].text = name;
        }
        else
        {
            Debug.Log("Steam API init -- failure ...");
        }
    }

    private void OnEnable()
    {
        if (SteamManager.Initialized) {
            Debug.Log("Enabled!");

            Callback_lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
            Callback_lobbyList = Callback<LobbyMatchList_t>.Create(OnGetLobbiesList);
            Callback_lobbyEnter = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
            Callback_lobbyInfo = Callback<LobbyDataUpdate_t>.Create(OnGetLobbyInfo);
            Callback_joinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequested);
            Callback_chatUpdate = Callback<LobbyChatUpdate_t>.Create(OnChatUpdate);
        }
    }

    /*/ Start is called before the first frame update
    void Start()
    {
        // We start by hiding the lobby canvas, this will be shown later
        lobby_canvas.enabled = false;

        if(SteamManager.Initialized)
        {
            name = SteamFriends.GetPersonaName();
            player_labels[0].text = name;
            LobbyCreated_t lobby_status = (LobbyCreated_t) SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, 4);
        }
        
    }*/

    public void OnPlayButtonClick()
    {
        SteamAPICall_t lobby_status = SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, 4);
    }

    public void OnBackButtonClick()
    {
        lobby_canvas.enabled = false;
        main_menu_canvas.enabled = true;
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    void OnLobbyCreated(LobbyCreated_t result)
    {
        if (result.m_eResult == EResult.k_EResultOK)
        {
            Debug.Log("Lobby created -- SUCCESS!");
            lobby_canvas.enabled = true;
            main_menu_canvas.enabled = false;
        }
        else
        {
            Debug.Log("Lobby created -- failure ...");
        }

        string personalName = SteamFriends.GetPersonaName();
        SteamMatchmaking.SetLobbyData((CSteamID)result.m_ulSteamIDLobby, "name", personalName+"'s game");
    }

    void OnGetLobbiesList(LobbyMatchList_t result)
    {
        Debug.Log("Found " + result.m_nLobbiesMatching + " lobbies!");
        for(int i=0; i< result.m_nLobbiesMatching; i++)
        {
            CSteamID lobbyID = SteamMatchmaking.GetLobbyByIndex(i);
            lobbyIDS.Add(lobbyID);
            SteamMatchmaking.RequestLobbyData(lobbyID);
        }
    }

    void OnGetLobbyInfo(LobbyDataUpdate_t result)
    {
        for(int i=0; i<lobbyIDS.Count; i++)
        {
            if (lobbyIDS[i].m_SteamID == result.m_ulSteamIDLobby)
            {
                Debug.Log("Lobby " + i+" :: " +SteamMatchmaking.GetLobbyData((CSteamID)lobbyIDS[i].m_SteamID, "name"));
                return;
            }
        }
       
    }

    void OnLobbyEntered(LobbyEnter_t result)
    {
        current_lobbyID = result.m_ulSteamIDLobby;

        if (result.m_EChatRoomEnterResponse == 1)
        {
            main_menu_canvas.enabled = false;
            lobby_canvas.enabled = true;
            Debug.Log("Lobby joined!");
            lobbyID = (Steamworks.CSteamID) current_lobbyID;
            players_in_lobby = SteamMatchmaking.GetNumLobbyMembers(lobbyID);
            update_lobby_labels();
        }   
        else
        {
            Debug.Log("Failed to join lobby.");
        }
    }

    void OnJoinRequested(GameLobbyJoinRequested_t result)
    {
        SteamMatchmaking.JoinLobby(result.m_steamIDLobby);
    }

    void OnChatUpdate(LobbyChatUpdate_t result)
    {
        update_lobby_labels();
    }

    void update_lobby_labels()
    {
        for (int i = 0; i < players_in_lobby; i++)
        {
            CSteamID tmpID = SteamMatchmaking.GetLobbyMemberByIndex(lobbyID, i);
            player_labels[i].text = SteamFriends.GetFriendPersonaName(tmpID);
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
