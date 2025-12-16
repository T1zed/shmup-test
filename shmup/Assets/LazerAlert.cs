using UnityEngine;

public class LazerAlert : MonoBehaviour
{
    public float duration = 1f; 

    void Start()
    {
        Destroy(gameObject, duration);
    }
}
