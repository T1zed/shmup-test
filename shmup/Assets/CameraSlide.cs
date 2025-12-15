using UnityEngine;

public class CameraSlide : MonoBehaviour
{
    public float speed = 5.0f;

    void Update()
    {
        Moving(speed);
    }

    public void Moving(float moveSpeed)
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}

