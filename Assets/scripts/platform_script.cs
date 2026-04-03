using UnityEngine;

public class platform_script : MonoBehaviour
{
    public float jumpForce = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check the collision came from ABOVE the platform
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y < -0.5f) // contact from top
            {
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                }
                break;
            }
        }
    }
}