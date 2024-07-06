using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerManager : MonoBehaviour
{
    [SerializeField] GameObject character;
    public void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(-89.5f, 63.5f), Random.Range(-31.5f, 15.5f));
        PhotonNetwork.Instantiate(character.name, randomPosition, Quaternion.identity);
    }
}
