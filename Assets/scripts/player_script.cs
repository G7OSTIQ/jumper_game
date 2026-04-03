using UnityEngine;
using UnityEngine.InputSystem;

public class player_script : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    private float moveX;
    private float minX;
    private float maxX;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        // ✅ calculate screen edges in world coordinates
        Camera cam = Camera.main;
        minX = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        maxX = cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
    }

    void Update()
    {
        moveX = Keyboard.current.dKey.isPressed ? 1f :
            Keyboard.current.aKey.isPressed ? -1f : 0f;
        moveX *= speed;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveX, rb.linearVelocity.y);
        // ✅ clamp player position within screen bounds
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}