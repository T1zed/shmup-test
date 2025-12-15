using UnityEngine;

public class EnemyMoveZigzag : MonoBehaviour
{
    private bool goingDown = true;
    private EnemyBehavior enemy;

    void Start()
    {
        enemy = GetComponent<EnemyBehavior>();
    }

    void Update()
    {
        if (enemy == null) return;

        transform.Translate(Vector3.right * enemy.moveSpeed * Time.deltaTime);

        float direction = goingDown ? -1f : 1f;
        transform.Translate(Vector3.up * direction * enemy.verticalSpeed * Time.deltaTime);

        float topY = 7f;
        float bottomY = -7f;
        if (transform.position.y <= bottomY) goingDown = false;
        if (transform.position.y >= topY) goingDown = true;
    }
}
