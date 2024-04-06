using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1.0";
    private RoomOptions roomOptions;

    void Start()
    {
        roomOptions = new RoomOptions { IsOpen = true, IsVisible = true, MaxPlayers = 4 };
        PhotonNetwork.AutomaticallySyncScene = true;
        Connect();
    }

    void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // Modified to incorporate room options
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    // Matchmaking
    public void JoinOrCreateRoom(string roomName)
    {
        // Try joining the room. If it doesn't exist, create it.
        RoomOptions roomOptions = new RoomOptions() { MaxPlayers = 4, IsOpen = true, IsVisible = true };
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    // Friends Invite
    public void InvitePlayerToRoom(string friendUserId)
    {
        PhotonNetwork.FindFriends(new string[] { friendUserId });
    }
    
    // Player Matchmaking
    public void QueueIntoMatchmaking()
    {
        // Details for the matchmaking can be defined here
        PhotonNetwork.JoinRandomRoom();
    }

    // Reconnection mechanism 
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected due to " + cause + ", reconnecting...");
        Connect();
    }

    // Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon servers!");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created a room!");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room!");

        // Sync players when a room is joined
        if (PhotonNetwork.IsConnectedAndReady)
            PhotonNetwork.Instantiate("PlayerPrefab", Vector3.zero, Quaternion.identity);
    }
}