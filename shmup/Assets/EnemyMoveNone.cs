using UnityEngine;

public class EnemyMoveNone : MonoBehaviour
{
    private float camHalfWidth;

    void Start()
    {
        camHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    void Update()
    {
        if (transform.position.x < Camera.main.transform.position.x - camHalfWidth  -7f)
        {
            Destroy(gameObject);
        }

    }
}

