using UnityEngine;
using UnityEngine.InputSystem;

public class player_script : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    private float moveX;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }
}