using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField _nickNameInputField;

    public void Start()
    {
        _nickNameInputField.text = PlayerPrefs.GetString("name");
        PhotonNetwork.NickName = _nickNameInputField.text;
    }
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
        CreateRoom();
    }

    public void SaveName()
    {
        PlayerPrefs.SetString("name", _nickNameInputField.text);
        PhotonNetwork.NickName = _nickNameInputField.text;
    }

    private void CreateRoom()
    {
        // Настройки новой комнаты
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 6; // Установите максимальное количество игроков в комнате
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;

        string roomName = "Room_" + Random.Range(1000, 10000);
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }
}