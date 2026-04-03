using UnityEngine;

public class jellyfish_part : MonoBehaviour
{
    public bool isHead = true; // ✅ check this on Head, uncheck on Stinger

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        if (isHead)
        {
            // ✅ big boost upward
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 15f);
        }
        else
        {
            // ✅ stinger kills player
            game_over_manager.instance.ShowGameOver();
        }
    }
}