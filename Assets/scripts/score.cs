using TMPro;
using UnityEngine;

public class score : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // drag your UI text in Inspector
    private float highestY;
    private float meter;

    void Start()
    {
        highestY = Camera.main.transform.position.y;
    }

    void Update()
    {
        // Only increase score when camera goes higher
        if (Camera.main.transform.position.y > highestY)
        {
            highestY = Camera.main.transform.position.y;
            meter = Mathf.RoundToInt(highestY * 10f); // multiply to get bigger numbers
        }

        scoreText.text = meter + "m";
    }
}
