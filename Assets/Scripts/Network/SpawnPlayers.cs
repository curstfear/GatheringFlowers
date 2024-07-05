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
        // При входе нового игрока проверяем, если мы ведущий, создаем ему объект игрока
        if (PhotonNetwork.IsMasterClient)
        {
            SpawnPlayer();
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // При выходе игрока проверяем, если мы ведущий, уничтожаем его объект игрока
        if (PhotonNetwork.IsMasterClient)
        {
            // Получаем объект игрока, который нужно уничтожить
            GameObject playerObject = GameObject.Find("PlayerPrefab(Clone)");

            // Проверяем, что объект найден
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
