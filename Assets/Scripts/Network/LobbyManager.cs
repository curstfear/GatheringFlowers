using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviourPunCallbacks
{
    private PhotonView photonView;

    private void Start()
    {
        // Получаем компонент PhotonView
        photonView = GetComponent<PhotonView>();

        // Проверяем, что PhotonView существует
        if (photonView == null)
        {
            Debug.LogError("PhotonView is not attached to the object.");
        }
    }

    private void Update()
    {
        // Проверяем подключение к комнате и наличие PhotonView
        if (PhotonNetwork.CurrentRoom != null && photonView != null)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                RequestSceneChange("Flowers");
            }
        }
    }

    [PunRPC]
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void RequestSceneChange(string sceneName)
    {
        if (photonView != null)
        {
            photonView.RPC("ChangeScene", RpcTarget.All, sceneName);
        }
        else
        {
            Debug.LogError("PhotonView is null in RequestSceneChange.");
        }
    }
}
