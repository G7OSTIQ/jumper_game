using UnityEngine;

public class jellyfish_script : MonoBehaviour
{
    public float boostForce = 12f; // higher than normal platform jump
    public string headColliderName = "Head";    // name your head child object
    public string stingerColliderName = "Stinger"; // name your stinger child object

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        // Check which part of jellyfish was hit
        string hitObject = collision.otherCollider.gameObject.name;

        if (hitObject == headColliderName)
        {
            // ✅ Head hit — big boost upward
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, boostForce);
        }
        else if (hitObject == stingerColliderName)
        {
            // ✅ Stinger hit — game over
            game_over_manager.instance.ShowGameOver();
        }
    }
}