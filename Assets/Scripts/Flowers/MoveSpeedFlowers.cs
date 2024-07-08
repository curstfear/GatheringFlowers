using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveSpeedFlowers : MonoBehaviour
{
    [SerializeField] float moveSpeedAmount = 0.1f; //сколько дается мувспида бонусом при подборе
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PhotonView playerPhotonView = collision.GetComponent<PhotonView>();
            if (playerPhotonView != null)
            {
                // Вызываем RPC-метод на игроке для выдачи урона бонусом
                playerPhotonView.RPC("GiveMoveSpeed", RpcTarget.All, moveSpeedAmount);

                // Уничтожаем цветок после взаимодействия
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
