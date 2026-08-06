using System;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{

    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    EnemyState m_State;

    [SerializeField] float findDistance = 8.0f;

    Transform player;

    [SerializeField] float attackDistance = 3.0f;
    [SerializeField] float moveSpeed = 5.0f;

    CharacterController cc;

    float currentTime = 0;
    float attackDelay = 2.0f;

    [SerializeField] int attackPower = 3;
    [SerializeField] int hp = 15;

    void Start()
    {
        m_State = EnemyState.Idle;

        player = GameObject.Find("Player").transform;

        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        switch(m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack(); 
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                Damaged();
                break;
            case EnemyState.Die:
                Die();
                break;
        }
    }

    void Idle()
    {
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State = EnemyState.Move;
            print("상태 전환: Idle -> Move");
        }
    }

    void Move()
    {
        // 플레이어와 거리가 공격 범위 밖이면 플레이어를 향해 이동
        if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;

            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            m_State = EnemyState.Attack;
            print("상태 전환: Move -> Attack");
            currentTime = attackDelay;
        }
    }

    void Attack()
    {
        // 공격 범위 내 있으면 공격
        if (Vector3.Distance(transform.position, player.position) <  attackDistance)
        {
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                player.GetComponent<PlayerMove>().DamageAction(attackPower);
                print("공격");
                currentTime = 0;
            }
        }
        else
        {
            m_State = EnemyState.Move;
            print("상태 전환: Attack -> Move");
            currentTime = 0;
        }
    }

    void Return()
    {

    }

    void Damaged()
    {

    }

    void Die()
    {

    }
}
