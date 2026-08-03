using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePostion;

    public GameObject bombFactory;

    // ÅõÃ´ ÆÄ¿ö
    public float throwPower = 15f;

    public GameObject bulletEffect;

    ParticleSystem ps;

    void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    void Update()
    {
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
                bulletEffect.transform.position = hitInfo.point;

                ps.Play();
            }
        }
    }
}
