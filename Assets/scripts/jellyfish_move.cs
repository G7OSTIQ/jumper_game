using UnityEngine;

public class jellyfish_move : MonoBehaviour
{
    public float moveSpeed = 1f;      // slow upward speed
    public float sidewaysSpeed = 0.8f;
    public float sidewaysRange = 2f;  // how far left/right it drifts
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // move upward slowly
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }
}