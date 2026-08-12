using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    int maxHp = 15;

    [SerializeField] Slider hpSlider;

    Vector3 originPos;

    [SerializeField] float moveDistance = 20f;

    Animator anim;

    void Start()
    {
        m_State = EnemyState.Idle;

        player = GameObject.Find("Player").transform;

        cc = GetComponent<CharacterController>();

        originPos = transform.position;

        anim = transform.GetComponentInChildren<Animator>();
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
                //Damaged();
                break;
            case EnemyState.Die:
                //Die();
                break;
        }
    }

    void Idle()
    {
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State = EnemyState.Move;
            print("상태 전환: Idle -> Move");

            anim.SetTrigger("IdleToMove");
        }
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            m_State = EnemyState.Return;
            print("상태 전환: Move -> Return");
        }
        // 플레이어와 거리가 공격 범위 밖이면 플레이어를 향해 이동
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;

            cc.Move(dir * moveSpeed * Time.deltaTime);

            transform.forward = dir;
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
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = originPos;

            hp = 15;
            m_State = EnemyState.Idle;
            print("상태 전환: Return -> Idle");
        }
    }

    void Damaged()
    {
        StartCoroutine(DamageProcess());
    }

    void Die()
    {
        StopAllCoroutines();
        StartCoroutine(DieProcess());
    }

    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(0.5f);

        m_State = EnemyState.Move;
        print("상태 전환: Damaged -> Move");
    }

    IEnumerator DieProcess()
    {
        cc.enabled = false;

        yield return new WaitForSeconds(2f);
        print("소멸!");
        Destroy(gameObject);
    }

    public void HitEnemy(int hitPower)
    {
        if (m_State == EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)
        {
            return;
        }
        
        hp -= hitPower;
        hpSlider.value = (float)hp / (float)maxHp;

        if (hp > 0)
        {
            print($"상태 전환: {m_State} -> Damaged");
            m_State = EnemyState.Damaged;
            Damaged();
        }
        else
        {
            print($"상태 전환: {m_State} -> Die");
            m_State = EnemyState.Die;
            Die();
        }
    }
}
