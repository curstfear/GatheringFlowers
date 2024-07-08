using Photon.Pun;
using UnityEngine;

public class HealthFlower : MonoBehaviour
{
    [SerializeField] int maxHealthAmount = 10; // ���������� ��������� ��������

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
                // �������� RPC-����� �� ������ ��� ������ ������ ��������
                playerPhotonView.RPC("GiveMaxHealth", RpcTarget.All, maxHealthAmount);

                // ���������� ������ ����� ��������������
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
