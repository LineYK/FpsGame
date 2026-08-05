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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_State = EnemyState.Idle;
    }

    // Update is called once per frame
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

    }

    void Move()
    {

    }

    void Attack()
    {

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
