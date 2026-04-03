using UnityEngine;

public class background_loop : MonoBehaviour
{
    private Camera mainCamera;
    private float backgroundHeight;
    private float startOffsetX;

    void Start()
    {
        mainCamera = Camera.main;
        backgroundHeight = GetComponent<SpriteRenderer>().bounds.size.y;
        startOffsetX = transform.position.x - mainCamera.transform.position.x;
    }

    void LateUpdate()
    {
        float cameraY = mainCamera.transform.position.y;
        float distanceFromCamera = transform.position.y - cameraY;

        // ✅ use 0.9f instead of 1f to create slight overlap, removing the gap
        // ✅ reduce multiplier so tiles reposition before gap appears
        if (distanceFromCamera < -backgroundHeight * 0.85f) // try 0.8f or 0.85f
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + backgroundHeight * 1.95f, // ✅ slightly less than 2f
                transform.position.z
            );
        }

        transform.position = new Vector3(
            mainCamera.transform.position.x + startOffsetX,
            transform.position.y,
            transform.position.z
        );
    }
}