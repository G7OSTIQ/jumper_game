using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class game_over_manager : MonoBehaviour
{
    public static game_over_manager instance;

    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;

    void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false); // hidden at start
    }

    public void ShowGameOver()
    {
        // get score from score manager
        float meter = score.instance.meter;
        finalScoreText.text = "Score: " + meter + "m";
        score.instance.HideScoreUI(); 
        gameOverPanel.SetActive(true); // show panel
        Time.timeScale = 0f;          // freeze the game
    }

    public void Restart()
    {
        Time.timeScale = 1f;          // unfreeze before reload
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}