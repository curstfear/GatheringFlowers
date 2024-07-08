using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlowers : MonoBehaviourPun
{
    public float minX, maxX, minY, maxY;
    [SerializeField] private GameObject flowerPrefab; // Prefab для цветка

    private PhotonView _photonView;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _spawnMaxDelay = 1f;
    [SerializeField] private float _flowerLifetime = 5f;

    void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        SpawnFlower();
    }

    private void SpawnFlower()
    {
        _spawnDelay -= Time.deltaTime;
        if (_spawnDelay <= 0)
        {
            Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            GameObject flowerInstance = PhotonNetwork.Instantiate(flowerPrefab.name, randomPosition, Quaternion.identity);
            StartCoroutine(DestroyFlowerAfterDelay(flowerInstance));
            _spawnDelay = _spawnMaxDelay;
        }
    }
    private IEnumerator DestroyFlowerAfterDelay(GameObject flower)
    {
        yield return new WaitForSeconds(_flowerLifetime);
        PhotonNetwork.Destroy(flower);
    }
}
