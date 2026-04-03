using UnityEngine;

public class Camera_follow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;

    private void LateUpdate()
    {
        if (target == null) return;

        // Only move up, never down, never left/right
        if (target.position.y > transform.position.y)
        {
            float targetY = target.position.y;

            Vector3 newPosition = new Vector3(
                transform.position.x,  // ✅ keep camera X fixed
                targetY,               // ✅ only follow Y upward
                transform.position.z
            );

            transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed * Time.deltaTime);
        }
    }
}