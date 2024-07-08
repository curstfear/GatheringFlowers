using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviourPun
{
    private PhotonView _photonView;
    private CameraFollow _camera;
    public Text _nickName;
    [SerializeField] private Image _HealthFill;
    CharacterController _characterController;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _photonView = GetComponent<PhotonView>();
        DontDestroyOnLoad(gameObject);
        if (_photonView.IsMine)
        {
            _camera = Camera.main.GetComponent<CameraFollow>();
            _camera.Initialize(gameObject.transform);
          
            _HealthFill.color = Color.green;
        }

        _nickName.text = _photonView.Owner.NickName;
        _nickName.text = _nickName.text.ToUpper();
    }
    [PunRPC]
    public void GiveDamage(int damage)//добавление урона игроку при подборе цветов
    {
        _characterController._characterDamage += damage;
        Debug.Log(_characterController._characterDamage);
    } 

}
