using UnityEngine;

public class Billboaed : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Awake()
    {
        if (target == null)
        {
            target = Camera.main.transform;
        }
    }

    void Update()
    {
        transform.forward = target.forward;
    }
}
