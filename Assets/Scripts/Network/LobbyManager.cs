using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private int requiredPlayers = 2;

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        CheckPlayersAndStartGame();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player entered room. Current player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
        CheckPlayersAndStartGame();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player left room. Current player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
        CheckPlayersAndStartGame();
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
}
