using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // ��������� PhotonNetwork.DontDestroyOnLoad ��� ������� ������
        DontDestroyOnLoad(gameObject);

        if (photonView.IsMine)
        {
            // ��� ��� ����������� �����
            Debug.Log("This is my player instance.");
        }
        else
        {
            // ��� ������ �����
            Debug.Log("This is another player instance.");
        }
    }
}
