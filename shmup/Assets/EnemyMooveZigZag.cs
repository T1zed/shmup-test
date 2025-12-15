using UnityEngine;

public class EnemyMoveZigzag : MonoBehaviour
{
    public float moveSpeedX = 5f;
    public float moveSpeedY = 3f;
    public float topY = 7f;
    public float bottomY = -7f;

    private bool goingDown = true;

    void Update()
    {

        transform.Translate(Vector3.right * moveSpeedX * Time.deltaTime);

        float direction = goingDown ? -1f : 1f;
        transform.Translate(Vector3.up * direction * moveSpeedY * Time.deltaTime);

        if (transform.position.y <= bottomY) goingDown = false;
        if (transform.position.y >= topY) goingDown = true;
    }
}
