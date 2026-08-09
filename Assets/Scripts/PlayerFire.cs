using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePostion;

    public GameObject bombFactory;

    public GameObject bulletEffect;
    
    ParticleSystem ps;
    
    // 투척 파워
    public float throwPower = 15f;
    
    // 발사 무기 공격력
    public int waponPower = 5;

    void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePostion.transform.position;

            Rigidbody rb = bomb.GetComponent<Rigidbody>();

            rb.AddForce(Camera.main.transform.forward *  throwPower, ForceMode.Impulse);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo = new RaycastHit();

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                    eFSM.HitEnemy(waponPower);
                }
                else
                {
                    bulletEffect.transform.position = hitInfo.point;
                    // 피격 이펙트의 forward 방향을 레이가 부딪힌 지점의 범선 벡터와 일치
                    bulletEffect.transform.forward = hitInfo.normal;

                    ps.Play();
                }
            }
        }
    }
}
