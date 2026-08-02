using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePostion;

    public GameObject bombFactory;

    // ÅõÃ´ ÆÄ¿ö
    public float throwPower = 15f;

    void Start()
    {
        
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
    }
}
