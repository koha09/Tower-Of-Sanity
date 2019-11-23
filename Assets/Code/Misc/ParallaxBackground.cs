using UnityEngine;

/// <summary>
/// Script for parallax background
/// </summary>

public class ParallaxBackground : MonoBehaviour
{

    public float speed;

    private Camera cam;
    private Renderer render;

    void Start()
    {
        cam = Camera.main;
        render = GetComponent<Renderer>();
    }

    void Update()
    {
        Vector2 offset = new Vector2(cam.transform.position.x * speed, 0f);
        render.material.mainTextureOffset = offset;
        transform.position = new Vector3(cam.transform.position.x, transform.position.y, transform.position.z);

    }
}
