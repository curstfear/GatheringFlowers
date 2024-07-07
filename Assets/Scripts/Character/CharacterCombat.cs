using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterCombat : MonoBehaviourPun
{
    [SerializeField] private Animator _animator;
    private bool isAttacking = false;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _meleeAttackRange = 0.5f;
    public int _characterDamage = 10;
    public LayerMask _playerLayer;

    private void Update()
    {
        if (photonView.IsMine && Input.GetMouseButton(0))
        {
            Attack();
            
        }
    }

    void Attack()
    {

        // �������� �������� ����� (���� ���������)
        _animator.SetTrigger("Attack"); // ���������� ����, ��� �������� ����� ��������

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _meleeAttackRange, _playerLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            // �������� PhotonView �������, �� ������� �������� �������
            PhotonView enemyPhotonView = enemy.GetComponent<PhotonView>();

            // ���������, ��� ������ ����� PhotonView � �� ����������� �������� ������
            if (enemyPhotonView != null && enemyPhotonView != photonView)
            {
                // ������� ���� ������ ������ �������
                enemyPhotonView.RPC("TakeDamage", RpcTarget.All, _characterDamage);
                isAttacking = false; // ����� 10 - ������ ���������� �����
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackPoint.position, _meleeAttackRange);
    }
}
