using UnityEngine;
using System.Collections;

public class EnemyMoveRandom : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float waitTime = 3f;
    private float camHalfWidth;
    private float camHalfHeight;

    void Start()
    {
        camHalfHeight = Camera.main.orthographicSize;
        camHalfWidth = camHalfHeight * Camera.main.aspect;
        StartCoroutine(MovePattern());
    }

    IEnumerator MovePattern()
    {
        Vector3 targetPos = GetRandomPointRightHalf();
        yield return StartCoroutine(MoveToPoint(targetPos));

        yield return new WaitForSeconds(waitTime);

        targetPos = GetRandomExitPoint();
        yield return StartCoroutine(MoveToPoint(targetPos));

        Destroy(gameObject);
    }

    IEnumerator MoveToPoint(Vector3 point)
    {
        while (Vector3.Distance(transform.position, point) > 0.1f)
        {
            Vector3 dir = (point - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;
            yield return null;
        }
        transform.position = point;
    }

    Vector3 GetRandomPointRightHalf()
    {
        float minX = Camera.main.transform.position.x;
        float maxX = Camera.main.transform.position.x + camHalfWidth;
        float minY = Camera.main.transform.position.y - camHalfHeight;
        float maxY = Camera.main.transform.position.y + camHalfHeight;

        return new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            0f
        );
    }

    Vector3 GetRandomExitPoint()
    {
        float minX = Camera.main.transform.position.x;
        float maxX = Camera.main.transform.position.x + camHalfWidth;
        float minY = Camera.main.transform.position.y - camHalfHeight;
        float maxY = Camera.main.transform.position.y + camHalfHeight;
        bool exitTop = Random.value > 0.5f;
        float exitY = exitTop ? maxY + 1f : minY - 1f;
        float exitX = Random.Range(minX, maxX);

        return new Vector3(exitX, exitY, 0f);
    }

}
