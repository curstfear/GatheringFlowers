using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveSpeedFlowers : MonoBehaviour
{
    [SerializeField] float moveSpeedAmount = 0.1f; //������� ������ �������� ������� ��� �������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PhotonView playerPhotonView = collision.GetComponent<PhotonView>();
            if (playerPhotonView != null)
            {
                // �������� RPC-����� �� ������ ��� ������ ����� �������
                playerPhotonView.RPC("GiveMoveSpeed", RpcTarget.All, moveSpeedAmount);

                // ���������� ������ ����� ��������������
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
