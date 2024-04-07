using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher instance;

    public int maxPlayers;
    public GameObject loadingScreen;
    public TMP_Text loadingText;
    public GameObject menuButtons;
    public GameObject createRoomScreen;
    public TMP_Text roomNameInput;
    public GameObject roomScreen;
    public TMP_Text roomNameText, playerNameLabel;
    public List<TMP_Text> playerNames = new();
    public GameObject errorScreen;
    public TMP_Text errorText;
    public GameObject nameInputScreen;
    public TMP_Text nameInput;
    private bool hasSetGamerTag;
    public GameObject startButton;

    public string levelToPlay;

    public GameObject roomBrowserScreen;
    public RoomButton roomButton; 
    private readonly List<RoomButton> _roomButtonList = new();
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CloseMenus();
        loadingScreen.SetActive(true);
        loadingText.text = "Connecting to network...";

        PhotonNetwork.ConnectUsingSettings();
    }

    private void CloseMenus()
    {
        loadingScreen.gameObject.SetActive(false);
        menuButtons.gameObject.SetActive(false);
        createRoomScreen.gameObject.SetActive(false);
        roomScreen.SetActive(false);
        errorScreen.SetActive(false);
        roomBrowserScreen.SetActive(false);
        nameInputScreen.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

        PhotonNetwork.AutomaticallySyncScene = true;

        loadingText.text = "Joining Lobby ...";
    }

    public override void OnJoinedLobby()
    {
        CloseMenus();
        menuButtons.SetActive(true);

        PhotonNetwork.NickName = Random.Range(0, 1000).ToString();

        if (!hasSetGamerTag)
        {
            CloseMenus();
            nameInputScreen.SetActive(true);

            if (PlayerPrefs.HasKey("gamerTag"))
            {
                nameInput.text = PlayerPrefs.GetString("gamerTag");
                
            }
            else
            {
                PhotonNetwork.NickName = PlayerPrefs.GetString("gamerTag");
            }
        }
    }

    public void OpenRoomCreate()
    {
        CloseMenus();
        createRoomScreen.SetActive(true);
    }

    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(roomNameInput.text))
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = maxPlayers;
            
            PhotonNetwork.CreateRoom(roomNameInput.text, options);
            
            CloseMenus();
            loadingText.text = "Creating Room ...";
            loadingScreen.SetActive(true);
        }
    }
    
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Failed to create room: " + message;
        CloseMenus();
        errorScreen.SetActive(true);
    }

    public void CloseErrorScreen()
    {
        CloseMenus();
        menuButtons.SetActive(true);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        CloseMenus();
        loadingText.text = "Leaving Room";
        loadingScreen.SetActive(true);
    }

    public override void OnLeftRoom()
    {
        CloseMenus();
        menuButtons.SetActive(true);
    }

    public void OpenRoomBrowser()
    {
        CloseMenus();
        roomBrowserScreen.SetActive(true);
    }

    public void CloseRoomBrowser()
    {
        CloseMenus();
        menuButtons.SetActive(true);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var rb in _roomButtonList)
        {
            Destroy(rb.gameObject);
        }
        _roomButtonList.Clear();
        
        roomButton.gameObject.SetActive(false);

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].PlayerCount <= roomList[i].MaxPlayers
                && !roomList[i].RemovedFromList)
            {
                var newButton = Instantiate(roomButton, roomButton.transform.parent);
                newButton.SetButtonDetails(roomList[i]);
                newButton.gameObject.SetActive(true);
                
                _roomButtonList.Add(newButton);
            }
        }
    }

    public void JoinRoom(RoomInfo inputInfo)
    {
        PhotonNetwork.JoinRoom(inputInfo.Name);
        CloseMenus();
        loadingText.text = "Joining Room ...";
        loadingScreen.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        CloseMenus();
        roomScreen.SetActive(true);
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        ListPlayers();

        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }

    private void ListPlayers()
    {
        foreach (var p in playerNames)
        {
            Destroy(p.gameObject);
        }
        playerNames.Clear();
        Player[] players = PhotonNetwork.PlayerList;
        foreach (var t in players)
        {
            TMP_Text newPlayerLabel = Instantiate(playerNameLabel, playerNameLabel.transform.parent);
            newPlayerLabel.text = t.NickName;
            newPlayerLabel.gameObject.SetActive(true);
            
            playerNames.Add(newPlayerLabel);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        TMP_Text newPlayerLabel = Instantiate(playerNameLabel, playerNameLabel.transform.parent);
        newPlayerLabel.text = newPlayer.NickName;
        newPlayerLabel.gameObject.SetActive(true);
            
        playerNames.Add(newPlayerLabel);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ListPlayers();
    }

    public override void OnMasterClientSwitched(Player player)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(levelToPlay);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetGamerTag()
    {
        if (!string.IsNullOrEmpty(nameInput.text))
        {
            PhotonNetwork.NickName = nameInput.text;
            
            PlayerPrefs.SetString("gamerTag", nameInput.text);
            
            CloseMenus();
            menuButtons.SetActive(true);
            hasSetGamerTag = true;
        }
    }
}