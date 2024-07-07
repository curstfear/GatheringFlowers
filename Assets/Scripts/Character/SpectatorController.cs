using Photon.Pun;
using UnityEngine;

public class ObserverController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float lookSpeed = 2f;

    private float yaw = 0f;
    private float pitch = 0f;
    private Camera observerCamera;

    private GameObject[] players;
    private int currentPlayerIndex = 0;

    public void InitializeAsObserver()
    {
        // �������������� ���������� ������ �� �����
        observerCamera = Camera.main;

        if (observerCamera != null)
        {
            observerCamera.enabled = true;
        }
        else
        {
            Debug.LogError("MainCamera not found!");
        }

        // ������ ������ ����
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // ����� ���� �������
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            FollowPlayer(players[currentPlayerIndex]);
        }
    }

    void Update()
    {
        if (observerCamera != null && observerCamera.enabled)
        {
            HandleMovement();
            HandleRotation();

            // ������������ ����� ��������
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SwitchPlayer(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SwitchPlayer(1);
            }
        }
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; // �������� �����/������
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime; // �������� ������/�����
        float moveY = 0f;

        if (Input.GetKey(KeyCode.Q))
        {
            moveY = -moveSpeed * Time.deltaTime; // �������� ����
        }
        else if (Input.GetKey(KeyCode.E))
        {
            moveY = moveSpeed * Time.deltaTime; // �������� �����
        }

        transform.Translate(new Vector3(moveX, moveY, moveZ));
    }

    void HandleRotation()
    {
        yaw += lookSpeed * Input.GetAxis("Mouse X");
        pitch -= lookSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }

    void SwitchPlayer(int direction)
    {
        if (players.Length > 0)
        {
            currentPlayerIndex = (currentPlayerIndex + direction + players.Length) % players.Length;
            FollowPlayer(players[currentPlayerIndex]);
        }
    }

    void FollowPlayer(GameObject player)
    {
        if (player != null)
        {
            transform.position = player.transform.position;
            transform.rotation = player.transform.rotation;
        }
    }
}
