using Photon.Pun;
using UnityEngine;

public class HealthFlower : MonoBehaviour
{
    [SerializeField] int maxHealthAmount = 10; // количество бонусного здоровья

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Entered");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player detected");
            PhotonView playerPhotonView = collision.GetComponent<PhotonView>();
            if (playerPhotonView != null)
            {
                Debug.Log("PhotonView found on player, sending RPC");
                // Вызываем RPC-метод на игроке для выдачи бонуса здоровья
                playerPhotonView.RPC("GiveMaxHealth", RpcTarget.All, maxHealthAmount);

                // Уничтожаем цветок после взаимодействия
                PhotonNetwork.Destroy(gameObject);
            }
            else
            {
                Debug.Log("No PhotonView found on player");
            }
        }
        else
        {
            Debug.Log("Collider does not have Player tag");
        }
    }
}
