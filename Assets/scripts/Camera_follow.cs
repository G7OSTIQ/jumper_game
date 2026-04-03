using UnityEngine;

public class Camera_follow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public float downDriftSpeed = 0.5f; // ✅ how fast camera drifts down

    private float highestY;

    void Start()
    {
        highestY = transform.position.y;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        if (target.position.y > transform.position.y)
        {
            // ✅ player going up — follow smoothly
            highestY = target.position.y;
            Vector3 newPosition = new Vector3(
                transform.position.x,
                highestY,
                transform.position.z
            );
            transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed * Time.deltaTime);
        }
        else
        {
            // ✅ player not going higher — slowly drift down toward player
            Vector3 driftPosition = new Vector3(
                transform.position.x,
                target.position.y,
                transform.position.z
            );
            transform.position = Vector3.Lerp(transform.position, driftPosition, downDriftSpeed * Time.deltaTime);
        }
    }
}