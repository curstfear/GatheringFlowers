using UnityEngine;

public class ObserverController : MonoBehaviour
{
    private Camera observerCamera;
    private GameObject[] players;
    private int currentPlayerIndex = 0;

    public float followSpeed = 5f;

    public void InitializeAsObserver()
    {
        // �������������� ���������� ������ �� �����
        observerCamera = Camera.main;

        if (observerCamera != null)
        {
            observerCamera.enabled = true;
            observerCamera.transform.SetParent(transform);
            observerCamera.transform.localPosition = new Vector3(0, 0, -10); // ���������� ������ ����� �� ��� Z
        }
        else
        {
            Debug.LogError("MainCamera not found!");
        }

        // ����� ���� �������
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            FollowPlayer(players[currentPlayerIndex]);
        }
    }

    void Update()
    {
        TrackingPLayer();
    }

    void TrackingPLayer()
    {
        if (observerCamera != null && observerCamera.enabled)
        {
            // ������������ ����� ��������
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SwitchPlayer(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SwitchPlayer(1);
            }

            // ���������, ��� ������� ������ ��������� � ���������� �������� ������� players
            if (currentPlayerIndex >= 0 && currentPlayerIndex < players.Length)
            {
                // ���������� �� ������� �������
                FollowPlayer(players[currentPlayerIndex]);
            }
        }
    }

    void SwitchPlayer(int direction)
    {
        if (players.Length > 0)
        {
            currentPlayerIndex = (currentPlayerIndex + direction + players.Length) % players.Length;
        }
    }

    void FollowPlayer(GameObject player)
    {
        if (player != null)
        {
            Vector3 targetPosition = player.transform.position;
            observerCamera.transform.position = Vector3.Lerp(observerCamera.transform.position, targetPosition + new Vector3(0, 0, -10), followSpeed * Time.deltaTime);
        }
    }
}
