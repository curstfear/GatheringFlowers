using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviourPunCallbacks
{
    public void OnJoinRoomButtonClicked()
    {
        PhotonNetwork.JoinOrCreateRoom("MatchmakingRoom", new Photon.Realtime.RoomOptions { MaxPlayers = 10 }, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room successfully.");
        SceneManager.LoadScene(2);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Join room failed: " + message);
    }
}