using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject character;
    public void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        PhotonNetwork.Instantiate(character.name, randomPosition, Quaternion.identity);
    }
}
