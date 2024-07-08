using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageFlowers : MonoBehaviour
{
    [SerializeField] int damageAmount = 5; //������� ������ ����� ������� ��� �������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PhotonView playerPhotonView = collision.GetComponent<PhotonView>();
            if (playerPhotonView != null)
            {
                // �������� RPC-����� �� ������ ��� ������ ����� �������
                playerPhotonView.RPC("GiveDamage", RpcTarget.All, damageAmount);

                // ���������� ������ ����� ��������������
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
