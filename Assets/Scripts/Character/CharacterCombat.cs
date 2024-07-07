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

        // Отыграть анимацию атаки (если требуется)
        _animator.SetTrigger("Attack"); // Установить флаг, что анимация атаки запущена

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _meleeAttackRange, _playerLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Получаем PhotonView объекта, на котором сработал триггер
            PhotonView enemyPhotonView = enemy.GetComponent<PhotonView>();

            // Проверяем, что объект имеет PhotonView и не принадлежит текущему игроку
            if (enemyPhotonView != null && enemyPhotonView != photonView)
            {
                // Наносим урон только другим игрокам
                enemyPhotonView.RPC("TakeDamage", RpcTarget.All, _characterDamage);
                isAttacking = false; // Здесь 10 - пример количества урона
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackPoint.position, _meleeAttackRange);
    }
}
