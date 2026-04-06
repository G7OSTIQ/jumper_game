using UnityEngine;

public class wall_follow : MonoBehaviour
{
    private Camera mainCamera;
    private float wallHeight;
    private float offsetX;

    void Start()
    {
        mainCamera = Camera.main;
        offsetX = transform.position.x - mainCamera.transform.position.x;
        wallHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void LateUpdate()
    {
        // ✅ keep X fixed to screen edge
        transform.position = new Vector3(
            mainCamera.transform.position.x + offsetX,
            transform.position.y,
            transform.position.z
        );

        // ✅ loop upward like background
        float distanceFromCamera = transform.position.y - mainCamera.transform.position.y;
        if (distanceFromCamera < -wallHeight * 0.9f)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + wallHeight * 1.95f,
                transform.position.z
            );
        }
    }
}