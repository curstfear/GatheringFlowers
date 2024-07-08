using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _characterSpeed;
    [SerializeField] private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _lastPosition;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _meleeAttackRange = 0.5f;
    private bool _canAttack = true;
    private bool _isAttacking = false;
    [SerializeField] private float _attackCooldown = 1f;
    public int _characterDamage = 10;
    public LayerMask _playerLayer;
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
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _lastPosition = transform.position;
    }

    void Update()
    {
        if (_photonView.IsMine)
        {
            Movement();
            CheckMovementState();
            if (Input.GetMouseButton(0) && _canAttack)
            {
                Debug.Log("Attack initiated");
                Attack();
            }
        }
    }

    void Attack()
    {
        Debug.Log("Setting state to attack");
        State = States.attack;
        _isAttacking = true;
        _canAttack = false;
        OnAttack();
        StartCoroutine(AttackAnimation());
        StartCoroutine(AttackCooldown());
    }
    public void OnAttack()
    {
        Debug.Log("OnAttack called");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _meleeAttackRange, _playerLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            PhotonView enemyPhotonView = enemy.GetComponent<PhotonView>();
            if (enemyPhotonView != null && enemyPhotonView != _photonView)
            {
                enemyPhotonView.RPC("TakeDamage", RpcTarget.All, _characterDamage);
            }
        }
    }
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(_attackCooldown);
        Debug.Log("Attack cooldown ended");
        _canAttack = true;
    }

    IEnumerator AttackAnimation()
    {
        // Продолжительность атаки может варьироваться, используйте реальную продолжительность вашей анимации
        yield return new WaitForSeconds(0.4f);
        _isAttacking = false;
        Debug.Log("Attack animation ended");
        State = States.idle;
    }

    void Movement()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 moveAmount = moveInput.normalized * _characterSpeed * Time.deltaTime;

        if (moveAmount.magnitude > 0 && !_isAttacking)
        {
            transform.position += (Vector3)moveAmount;
            State = States.run;

            if(moveInput.x < 0)
                _spriteRenderer.flipX = true;

            else 
                _spriteRenderer.flipX = false;

            if(moveInput.y > 0)
                State = States.runTop;

            else if(moveInput.y < 0)
                State = States.runDown;
        }

        else if(moveAmount.magnitude == 0 && !_isAttacking)
            State = States.idle;
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
    runTop,
    runDown,
    attack
}
