using UnityEngine;

public class moving_platform_script : MonoBehaviour
{
    public float moveSpeed = 2f;
    private float leftEdge;
    private float rightEdge;
    private bool movingRight = true;

    void Start()
    {
        // set movement bounds based on spawn position
        leftEdge = transform.position.x - Random.Range(1f, 3f);
        rightEdge = transform.position.x + Random.Range(1f, 3f);
    }

    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            if (transform.position.x >= rightEdge)
                movingRight = false;
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            if (transform.position.x <= leftEdge)
                movingRight = true;
        }
    }
}