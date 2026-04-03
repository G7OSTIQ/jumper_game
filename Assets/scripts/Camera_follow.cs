using UnityEngine;

public class Camera_follow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f; // adjust in Inspector

    private void LateUpdate()
    {
        if (target == null) return;

        // Only follow player when they go HIGHER than camera
        float targetY = transform.position.y;
        if (target.position.y > transform.position.y)
        {
            targetY = target.position.y;
        }

        Vector3 newPosition = new Vector3(
            target.position.x,           // follow X always
            targetY,                      // only move up on Y
            transform.position.z          // keep Z fixed
        );

        // Smooth the movement instead of snapping
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed * Time.deltaTime);
    }
}