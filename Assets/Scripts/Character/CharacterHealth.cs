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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _characterHealth -= 50;
            UpdateHealthUI();
        }
    }

    void UpdateHealthUI()
    {
        _healthFill.fillAmount = _characterHealth / _characterMaxHealth;
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
        }
    }
}
