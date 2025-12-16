using UnityEngine;

public class BossMooving : MonoBehaviour
{
    public float offsetX = 8f;          
    public float verticalSpeed = 3f;    
    public float minY = -6f;
    public float maxY = 6f;

    private Camera cam;
    private bool goingUp = true;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (cam == null) return;


        Vector3 pos = transform.position;
        pos.x = cam.transform.position.x + offsetX;

        float dir = goingUp ? 1f : -1f;
        pos.y += dir * verticalSpeed * Time.deltaTime;

        if (pos.y >= maxY) goingUp = false;
        if (pos.y <= minY) goingUp = true;

        transform.position = pos;
    }
}
