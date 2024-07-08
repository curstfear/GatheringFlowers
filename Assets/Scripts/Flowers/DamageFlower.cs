using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageFlowers : MonoBehaviour
{
    [SerializeField] int damageAmount = 5; //сколько дается урона бонусом при подборе
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PhotonView playerPhotonView = collision.GetComponent<PhotonView>();
            if (playerPhotonView != null)
            {
                // Вызываем RPC-метод на игроке для выдачи урона бонусом
                playerPhotonView.RPC("GiveDamage", RpcTarget.All, damageAmount);

                // Уничтожаем цветок после взаимодействия
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
