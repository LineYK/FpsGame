using UnityEngine;

public class BombAction : MonoBehaviour
{
    // 폭발 이펙트 프리팹
    public GameObject bombEffect;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // OnCollisionEnter는 이 collider/rigidbody가 다른 rigidbody/collider에 접촉되기 시작하면 호출됩니다.
    private void OnCollisionEnter(Collision collision)
    {
        GameObject eff = Instantiate(bombEffect);

        eff.transform.position = transform.position;

        Destroy(gameObject);
    }


}
