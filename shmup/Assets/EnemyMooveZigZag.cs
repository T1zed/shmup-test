using UnityEngine;

public class EnemyMoveZigzag : MonoBehaviour
{
    private EnemyBehavior enemy;
    private float initialY;
    private float timeOffset;

    [Header("Sin Movement")]
    public float amplitude = 2f;      
    public float frequency = 1f;     

    void Start()
    {
        enemy = GetComponent<EnemyBehavior>();
        if (enemy == null) return;

        initialY = transform.position.y;
        timeOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        if (enemy == null) return;


        transform.Translate(Vector3.right * enemy.moveSpeed * Time.deltaTime);

        float newY = initialY + Mathf.Sin(Time.time * frequency + timeOffset) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
