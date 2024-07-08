using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterController : MonoBehaviourPun, IPunObservable
{
    public float _characterSpeed;
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

    private States State
    {
        get { return (States)_animator.GetInteger("state"); }
        set { _animator.SetInteger("state", (int)value); }
    }

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _lastPosition = transform.position;
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            Movement();
            CheckMovementState();
            if (Input.GetMouseButtonDown(0) && _canAttack) // Changed to GetMouseButtonDown for a single click
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        _isAttacking = true;
        _canAttack = false;
        OnAttack();
        photonView.RPC("PlayAttackAnimation", RpcTarget.All);
        StartCoroutine(AttackCooldown());
    }

    [PunRPC]
    void PlayAttackAnimation()
    {
        State = States.attack;
        StartCoroutine(AttackAnimation());
    }

    public void OnAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _meleeAttackRange, _playerLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            PhotonView enemyPhotonView = enemy.GetComponent<PhotonView>();
            if (enemyPhotonView != null && enemyPhotonView != photonView)
            {
                enemyPhotonView.RPC("TakeDamage", RpcTarget.All, _characterDamage);
            }
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }

    IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.4f); // Duration of the attack animation
        _isAttacking = false;
        if (!_isMoving) // Check if the character is moving or not
        {
            State = States.idle;
            photonView.RPC("SyncIdleState", RpcTarget.All);
        }
    }

    [PunRPC]
    void SyncIdleState()
    {
        State = States.idle;
    }

    void Movement()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 moveAmount = moveInput.normalized * _characterSpeed * Time.deltaTime;

        if (moveAmount.magnitude > 0 && !_isAttacking)
        {
            transform.position += (Vector3)moveAmount;
            if (moveInput.x < 0)
            {
                _spriteRenderer.flipX = true;
                _attackPoint.transform.localPosition = new Vector3(-1, 0, 0);
            }
            else
            {
                _spriteRenderer.flipX = false;
                _attackPoint.transform.localPosition = new Vector3(1, 0, 0);
            }

            if (moveInput.y > 0)
            {
                _attackPoint.transform.localPosition = new Vector3(0, 1, 0);
                State = States.runTop;
                if(_isAttacking) State = States.attackTop;

            }
            else if (moveInput.y < 0)
            {
                _attackPoint.transform.localPosition = new Vector3(0, -1, 0);
                State = States.runDown;
                if (_isAttacking) State = States.attackDown;
            }

            else
                State = States.run;
        }
        else if (moveAmount.magnitude == 0 && !_isAttacking)
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(State);
        }
        else
        {
            State = (States)stream.ReceiveNext();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackPoint.position, _meleeAttackRange);
    }
}

public enum States
{
    idle,
    run,
    runTop,
    runDown,
    attack,
    attackTop,
    attackDown
}
