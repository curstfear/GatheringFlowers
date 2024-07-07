using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _characterSpeed;
    [SerializeField] private Animator _animator;
    private Vector2 _lastPosition;
    private bool _isMoving;
    PhotonView _photonView;

    private States State
    {
        get { return (States)_animator.GetInteger("state"); }

        set { _animator.SetInteger("state", (int)value); }
    }

    void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _animator = GetComponent<Animator>();
        _lastPosition = transform.position;
    }

    void Update()
    {
        if (_photonView.IsMine)
        {
            Movement();
            CheckMovementState();
        }
    }

    void Movement()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 moveAmount = moveInput.normalized * _characterSpeed * Time.deltaTime;

        if (moveAmount.magnitude > 0)
        {
            transform.position += (Vector3)moveAmount;
            State = States.run;
        }
        else
        {
            State = States.idle;
        }
    }

    void CheckMovementState()
    {
        if (Vector2.Distance(transform.position, _lastPosition) > 0.01f)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }

        _lastPosition = transform.position;
    }
}

public enum States
{
    idle,
    run,
    attack
}
