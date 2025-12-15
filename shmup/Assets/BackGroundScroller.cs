using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 0.5f; 

    private Material mat;
    private Vector2 offset;

    void Start()
    {

        mat = GetComponent<SpriteRenderer>().material;
        offset = mat.mainTextureOffset;
    }

    void Update()
    {

        offset.x += scrollSpeed * Time.deltaTime;
        mat.mainTextureOffset = offset;
    }
}
