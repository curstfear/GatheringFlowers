using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _characterSpeed;
    [SerializeField] private Animator _animator;
    PhotonView _photonView;

    void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (_photonView.IsMine)
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 moveAmount = moveInput.normalized * _characterSpeed * Time.deltaTime;
            transform.position += (Vector3)moveAmount;
            UpdateCharacterDirection(moveInput);
        }
    }
    void UpdateCharacterDirection(Vector2 moveInput)
    {
        // Сброс параметров анимации
        _animator.SetBool("isMovingUp", false);
        _animator.SetBool("isMovingDown", false);
        _animator.SetBool("isMovingRight", false);
        _animator.SetBool("isMovingLeft", false);

        if (moveInput.x > 0)
        {
            // Движение вправо
            transform.localScale = new Vector3(1, 1, 1); // Оригинальный масштаб
            _animator.SetBool("isMovingRight", true);
        }
        else if (moveInput.x < 0)
        {
            // Движение влево
            transform.localScale = new Vector3(-1, 1, 1); // Отражение по горизонтали
            _animator.SetBool("isMovingLeft", true);
        }
        else if (moveInput.y > 0)
        {
            _animator.SetBool("isMovingUp", true);
        }
        else if (moveInput.y < 0)
        {
            _animator.SetBool("isMovingDown", true);
        }
    }
}
