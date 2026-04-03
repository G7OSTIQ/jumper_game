using UnityEngine;

public class enemy_script : MonoBehaviour
{
    public float moveSpeed = 2f;
    private float platformLeftEdge;
    private float platformRightEdge;
    private bool movingRight = true;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Collider2D platformCollider = GetComponentInParent<Collider2D>();
        if (platformCollider != null)
        {
            platformLeftEdge = platformCollider.bounds.min.x;
            platformRightEdge = platformCollider.bounds.max.x;
        }
    }

    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            spriteRenderer.flipX = false;
            if (transform.position.x >= platformRightEdge)
                movingRight = false;
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            spriteRenderer.flipX = true;
            if (transform.position.x <= platformLeftEdge)
                movingRight = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ✅ player touches bug
        {
            game_over_manager.instance.ShowGameOver();
        }
    }
}