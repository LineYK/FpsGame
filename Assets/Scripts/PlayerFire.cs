using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    private enum WeaponMode
    {
        Normal,
        Sniper
    }

    public GameObject firePostion;

    public GameObject bombFactory;

    public GameObject bulletEffect;
    
    ParticleSystem ps;
    
    // 투척 파워
    public float throwPower = 15f;
    
    // 발사 무기 공격력
    public int waponPower = 5;

    private Animator anim;

    private WeaponMode wMode;

    private bool ZoomMode = false;

    [SerializeField]
    private TMP_Text wModeText;

    [SerializeField]
    private GameObject[] effFlash;

    [SerializeField]
    private GameObject[] weapons;

    [SerializeField]
    private GameObject[] crosshairs;

    [SerializeField]
    private List<GameObject> weaponRs;

    private GameObject curWeapon;
    private GameObject curCrosshair;
    private GameObject curWeaponRs;

    void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();

        anim = GetComponentInChildren<Animator>();

        wMode = WeaponMode.Normal;

        curWeapon = weapons[0];
        curCrosshair = crosshairs[0];
        curWeaponRs = weaponRs[0];
    }

    void Update()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        /*
         * 노멀 모드: 마우스 오른쪽 버튼을 누르면 시선방향으로 수류탄
         * 스나이퍼 모드: 마우스 오른쪽 버튼을 누르면 화면을 확대
         */
        if (Input.GetMouseButtonDown(1))
        {
            switch (wMode)
            {
                case WeaponMode.Normal:
                    GameObject bomb = Instantiate(bombFactory);
                    bomb.transform.position = firePostion.transform.position;

                    Rigidbody rb = bomb.GetComponent<Rigidbody>();

                    rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
                    break;
                case WeaponMode.Sniper:
                    if(!ZoomMode)
                    {
                        Camera.main.fieldOfView = 15f;
                        ZoomMode = true;
                    } 
                    else
                    {
                        Camera.main.fieldOfView = 60f;
                        ZoomMode = false;
                    }
                    break;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (anim.GetFloat("MoveMotion") == 0)
            {
                anim.SetTrigger("Attack");
            }

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
                    // 피격 이펙트의 forward 방향을 레이가 부딪힌 지점의 법선 벡터와 일치
                    bulletEffect.transform.forward = hitInfo.normal;

                    ps.Play();
                }
            }

            StartCoroutine(ShootEffectOn(0.05f));
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            wMode = WeaponMode.Normal;

            // 카메라 시야각 복구
            Camera.main.fieldOfView = 60f;

            wModeText.text = "Normal Mode";

            curWeapon.SetActive(false);
            curCrosshair.SetActive(false);
            curWeaponRs.SetActive(false);

            curWeapon = weapons[0];
            curCrosshair = crosshairs[0];
            curWeaponRs = weaponRs[0];

            curWeapon.SetActive(true);
            curCrosshair.SetActive(true);
            curWeaponRs.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            wMode = WeaponMode.Sniper;

            wModeText.text = "Sniper Mode";

            curWeapon.SetActive(false);
            curCrosshair.SetActive(false);
            curWeaponRs.SetActive(false);

            curWeapon = weapons[1];
            curCrosshair = crosshairs[1];
            curWeaponRs = weaponRs[1];

            curWeapon.SetActive(true);
            curCrosshair.SetActive(true);
            curWeapon.SetActive(true);
        }

    }

    // 총구 이펙트 코루틴
    private IEnumerator ShootEffectOn(float duration)
    {
        int num = Random.Range(0, effFlash.Length - 1);
        effFlash[num].SetActive(true);

        yield return new WaitForSeconds(duration);

        effFlash[num].SetActive(false); yield return new WaitForSeconds(duration);
    }
}
