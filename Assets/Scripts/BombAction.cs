using UnityEngine;

public class BombAction : MonoBehaviour
{
    // 폭발 이펙트 프리팹
    public GameObject bombEffect;

    // 수류탄 데미지
    [SerializeField]
    private int attackPower = 10;

    // 폭발 효과 반경
    [SerializeField]
    private float explostionRadius = 5f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // OnCollisionEnter는 이 collider/rigidbody가 다른 rigidbody/collider에 접촉되기 시작하면 호출됩니다.
    private void OnCollisionEnter(Collision collision)
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, explostionRadius, 1 << 8);

        foreach (Collider col in cols)
        {
            col.GetComponent<EnemyFSM>().HitEnemy(attackPower);
        }

        GameObject eff = Instantiate(bombEffect);

        eff.transform.position = transform.position;

        Destroy(gameObject);
    }


}
