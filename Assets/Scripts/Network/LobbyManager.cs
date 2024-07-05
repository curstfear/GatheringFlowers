using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private int requiredPlayers = 1; // Количество игроков для начала игры
    public string gameSceneName = "Flowers"; // Имя сцены, которую нужно загрузить

    private bool isConnecting;

    void Start()
    {
        // Подключение к Photon
        ConnectToPhoton();
    }

    void ConnectToPhoton()
    {
        isConnecting = true;
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting to Photon...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master server.");
        if (isConnecting)
        {
            CreateOrJoinRoom();
        }
    }

    void CreateOrJoinRoom()
    {
        Debug.Log("Creating or joining room...");
        PhotonNetwork.JoinOrCreateRoom("TestRoom", new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room successfully.");
        CheckPlayersAndStartGame();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("Player entered room. Current player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
        CheckPlayersAndStartGame();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log("Player left room. Current player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
        CheckPlayersAndStartGame();
    }

    void CheckPlayersAndStartGame()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == requiredPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false; // Закрыть комнату, чтобы новые игроки не могли присоединиться
            PhotonNetwork.CurrentRoom.IsVisible = false; // Сделать комнату невидимой
            PhotonNetwork.LoadLevel(gameSceneName); // Загрузить игровой уровень
        }
    }
}
