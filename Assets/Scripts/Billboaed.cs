using UnityEngine;

public class Billboaed : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Update()
    {
        transform.forward = target.forward;    
    }
}
