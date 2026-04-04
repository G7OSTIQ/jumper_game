using TMPro;
using UnityEngine;

public class score : MonoBehaviour
{
    public static score instance; // ✅ so other scripts can access it
    public GameObject scoreUI;
    public TextMeshProUGUI scoreText;
    private float highestY;
    public float meter;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        highestY = Camera.main.transform.position.y;
    }

    void Update()
    {
        if (Camera.main.transform.position.y > highestY)
        {
            highestY = Camera.main.transform.position.y;
            meter = Mathf.RoundToInt(highestY * 10f);
        }

        scoreText.text = meter + "m";
    }
    
    public void HideScoreUI()
    {
        scoreUI.SetActive(false); // ✅ hides the score UI
    }
}
