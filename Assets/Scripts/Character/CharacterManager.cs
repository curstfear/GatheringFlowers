using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviourPun, IPunObservable
{
    private PhotonView _photonView;
    private CameraFollow _camera;
    public Text _nickName;
    [SerializeField] private Image _HealthFill;
    public GameObject Flowers;

    public Text _countOfDamageFlowersText;
    public Text _countOfMoveSpeedFlowersText;
    public Text _countOfHealthFlowersText;

    private float _countOfDamageFlowers;
    private float _countOfMoveSpeedFlowers;
    private float _countOfHealthFlowers;

    CharacterController _characterController;
    CharacterHealth _characterHealth;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _characterHealth = GetComponent<CharacterHealth>();
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

    void Update()
    {
        ShowFlowersStat();
    }

    private void ShowFlowersStat()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
            Flowers.SetActive(true);

        else if (Input.GetKeyUp(KeyCode.LeftAlt))
            Flowers.SetActive(false);
    }

    [PunRPC]
    public void GiveDamage(int damage)//добавление урона игроку при подборе цветов
    {
        _characterController._characterDamage += damage;
        _countOfDamageFlowers++;
        _countOfDamageFlowersText.text = _countOfDamageFlowers.ToString();
        Debug.Log(_characterController._characterDamage);
    }

    [PunRPC]
    public void GiveMoveSpeed(float moveSpeed)//добавление мувспида игроку при подборе цветов
    {
        _characterController._characterSpeed += moveSpeed;
        _countOfMoveSpeedFlowers++;
        _countOfMoveSpeedFlowersText.text = _countOfMoveSpeedFlowers.ToString();
        Debug.Log(_characterController._characterDamage);
    }

    [PunRPC]
    public void GiveMaxHealth(int maxHealth)//добавление к макс. здоровья игрока при подборе цветов
    {
        _characterHealth._characterMaxHealth += maxHealth;
        _countOfHealthFlowers++;
        _countOfHealthFlowersText.text = _countOfHealthFlowers.ToString();
        Debug.Log(_characterController._characterDamage);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Отправка данных другим игрокам
            stream.SendNext(_countOfDamageFlowersText);
            stream.SendNext(_countOfMoveSpeedFlowersText);
            stream.SendNext(_countOfHealthFlowers);
        }
        else
        {
            // Получение данных от других игроков
            _countOfDamageFlowersText = (Text)stream.ReceiveNext();
            _countOfMoveSpeedFlowersText = (Text)stream.ReceiveNext();
            _countOfHealthFlowersText = (Text)stream.ReceiveNext();
        }
    }

}
