using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterCombat : MonoBehaviourPun
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _meleeAttackRange = 0.5f;
    private bool _canAttack = true;
    private bool _isAttacking = false;
    [SerializeField] private float _attackCooldown = 1f;
    public int _characterDamage = 10;
    public LayerMask _playerLayer;

    private States State
    {
        get { return (States)_animator.GetInteger("state"); }
        set { _animator.SetInteger("state", (int)value); }
    }

    private void Awake()
    {
        _canAttack = true;
    }

    private void Update()
    {
        if (photonView.IsMine && Input.GetMouseButton(0) && _canAttack)
        {
            Debug.Log("Attack initiated");
            Attack();
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
            if (enemyPhotonView != null && enemyPhotonView != photonView)
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


    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackPoint.position, _meleeAttackRange);
    }
}
