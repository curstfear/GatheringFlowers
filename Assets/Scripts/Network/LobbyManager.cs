using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private int requiredPlayers = 3;
    public Text playerInRoomCount;

    void Start()
    {
        UpdatePlayersCountText();
        PhotonNetwork.AutomaticallySyncScene = true;
        CheckPlayersAndStartGame();
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable()
            {
                {"PlayersCount", PhotonNetwork.CurrentRoom.PlayerCount}
            });
        }
    }

    private void UpdatePlayersCountText()
    {
        playerInRoomCount.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString() + "/" + requiredPlayers;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player entered room. Current player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
        UpdatePlayersCountText();
        CheckPlayersAndStartGame();
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable()
            {
                { "PlayersCount", PhotonNetwork.CurrentRoom.PlayerCount}
            });
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player left room. Current player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
        UpdatePlayersCountText();
        CheckPlayersAndStartGame();
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable()
            {
                { "PlayersCount", PhotonNetwork.CurrentRoom.PlayerCount}
            });
        }
    }

    private void CheckPlayersAndStartGame()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount >= requiredPlayers)
        {
            Debug.Log("Required players reached. Starting game...");
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel("Flowers");
        }
    }

    public void PrematureStart() // преждевременный старт
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel("Flowers");
    }
}
