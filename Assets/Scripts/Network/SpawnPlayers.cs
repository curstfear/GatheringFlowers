using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject player;
    public float minX, maxX, minY, maxY;

    private void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            SpawnPlayer();
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // ��� ����� ������ ������ ���������, ���� �� �������, ������� ��� ������ ������
        if (PhotonNetwork.IsMasterClient)
        {
            SpawnPlayer();
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // ��� ������ ������ ���������, ���� �� �������, ���������� ��� ������ ������
        if (PhotonNetwork.IsMasterClient)
        {
            // �������� ������ ������, ������� ����� ����������
            GameObject playerObject = GameObject.Find("PlayerPrefab(Clone)");

            // ���������, ��� ������ ������
            if (playerObject != null)
            {
                PhotonNetwork.Destroy(playerObject);
            }
        }
    }

        private void SpawnPlayer()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, minY), Random.Range(maxX, maxY));
        PhotonNetwork.Instantiate(player.name, randomPosition, Quaternion.identity);
    }

   

}
