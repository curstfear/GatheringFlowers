using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CharacterHealth : MonoBehaviourPunCallbacks
{
    [SerializeField] private Image _healthFill;
    [SerializeField] private float _characterMaxHealth;
    [SerializeField] private float _characterHealth;
    public GameObject SpectatorPrefab;

    private void Start()
    {
        _characterHealth = _characterMaxHealth;
        
    }

    private void Update()
    {
        TakeDamage();
    }

    private void TakeDamage()
    {
        if (!photonView.IsMine) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _characterHealth -= 50;
            if(_characterHealth <= 0)
            {
                _characterHealth = 0;
                Die();
            }
            UpdateHealthUI();
        }
        photonView.RPC("SyncHealth", RpcTarget.Others, _characterHealth);
    }

    void UpdateHealthUI()
    {
        _healthFill.fillAmount = _characterHealth / _characterMaxHealth;
    }

    [PunRPC]
    public void SyncHealth(float syncedHealth)
    {
        _characterHealth = syncedHealth;
        UpdateHealthUI();
    }

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_characterHealth);
        }

        if (stream.IsReading)
        {
            _characterHealth = (float)stream.ReceiveNext();
            UpdateHealthUI();
        }
    }
    void Die()
    {
        // Удаление игрока из сцены
        PhotonNetwork.Destroy(gameObject);

        // Создание наблюдателя
        GameObject Spectator = PhotonNetwork.Instantiate(SpectatorPrefab.name, transform.position, Quaternion.identity);
        Spectator.GetComponent<ObserverController>().InitializeAsObserver();
    }
}
