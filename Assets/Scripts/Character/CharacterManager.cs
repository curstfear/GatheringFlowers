using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // Применяем PhotonNetwork.DontDestroyOnLoad для объекта игрока
        DontDestroyOnLoad(gameObject);

        if (photonView.IsMine)
        {
            // Это наш собственный игрок
            Debug.Log("This is my player instance.");
        }
        else
        {
            // Это другой игрок
            Debug.Log("This is another player instance.");
        }
    }
}
