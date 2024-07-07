using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerManager : MonoBehaviour
{
    [SerializeField] GameObject character;
    public void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(-200.5f, -169.5f), Random.Range(7.5f, -18.5f));
        PhotonNetwork.Instantiate(character.name, randomPosition, Quaternion.identity);
    }
}
