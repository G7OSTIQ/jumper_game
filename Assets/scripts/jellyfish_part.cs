using UnityEngine;

public class jellyfish_part : MonoBehaviour
{
    public bool isHead = true;
    public float boostForce = 15f;
    private bool hasBoosted = false; // ✅ prevent spamming boost

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryBoost(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        TryBoost(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            hasBoosted = false; // ✅ reset when player leaves
    }

    private void TryBoost(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (hasBoosted) return; // ✅ only boost once per touch

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        if (isHead && rb.linearVelocity.y <= 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, boostForce);
            hasBoosted = true;
        }
    }
}