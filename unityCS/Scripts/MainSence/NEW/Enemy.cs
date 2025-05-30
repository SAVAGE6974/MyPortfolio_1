using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEWEnemy : MonoBehaviour
{
    [SerializeField] NEWUI mainUI;
    
    public Transform tower;
    public float speed = 3f;
    public float stopDistance = 1f;

    public float enemyHp = 100f;
    public static float AL_1S_damage = 100f;
    public static float Wakamo_damage = 80f;
    
    
    private bool isAttacking = false;

    private void Update()
    {
        GoAttack();
    }

    public void Enemyhit()
    {
        if (LockinManager.lastSelectedCharacter == "Wakamo") // wakamo
        {
            Debug.Log("Wakamo");
            enemyHp -= Wakamo_damage;
            if (enemyHp <= 0)
                gameObject.SetActive(false);
        }
        else if (LockinManager.lastSelectedCharacter == "AL-1S")
        {
            enemyHp -= AL_1S_damage;
            Debug.Log("EnemyHp" + enemyHp);
            if (enemyHp <= 0)
            {
                gameObject.SetActive(false);
            }
                
        }
    }

    private void GoAttack()
    {
        if (NEWUI.canGoAttack)
        {
            if (tower == null) return;

            Vector3 direction = tower.position - transform.position;
            direction.y = 0;

            float distance = direction.magnitude;

            if (distance > stopDistance)
            {
                // 이동
                Vector3 moveDir = direction.normalized;
                transform.position += moveDir * (speed * Time.deltaTime);
            }
            else
            {
                // 멈추고 타워 공격 시작
                if (!isAttacking)
                {
                    StartCoroutine(AttackTowerRoutine());
                }
            }
        }
    }
    
    private IEnumerator AttackTowerRoutine()
    {
        isAttacking = true;

        while (true)
        {
            mainUI.HitTower();
            yield return new WaitForSeconds(1f); // 1초마다 공격
        }
    }
}
