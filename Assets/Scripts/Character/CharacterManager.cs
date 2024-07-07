using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviourPun
{
    private PhotonView _photonView;
    private CameraFollow _camera;
    public Text _nickName;
    void Start()
    {
        
        _photonView = GetComponent<PhotonView>();
        DontDestroyOnLoad(gameObject);
        if (_photonView.IsMine)
        {
            _camera = Camera.main.GetComponent<CameraFollow>();
            _camera.Initialize(gameObject.transform);
            _nickName.color = Color.white;
        }

        _nickName.text = _photonView.Owner.NickName;
        _nickName.text = _nickName.text.ToUpper();
    }
}
