using Photon.Pun;
using UnityEngine;

public class CharacterManager : MonoBehaviourPun
{
    private PhotonView _photonView;
    private CameraFollow _camera;
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        DontDestroyOnLoad(gameObject);
        if (_photonView.IsMine)
        {
            _camera = Camera.main.GetComponent<CameraFollow>();
            _camera.Initialize(gameObject.transform);
        }
    }
}
