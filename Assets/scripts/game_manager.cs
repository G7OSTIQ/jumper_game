using UnityEngine;
using UnityEngine.SceneManagement;

public class game_manager : MonoBehaviour
{
    public GameObject platformPrefab;
    public int platformCount = 300;
    public Transform player; // ✅ drag your player in Inspector

    void Start()
    {
        Vector3 spawnPosition = new Vector3(0f, 1f, 0f);

        for (int i = 0; i < platformCount; i++)
        {
            spawnPosition.y += Random.Range(1.8f, 2.5f);
            spawnPosition.x = Random.Range(-3f, 3f);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void Update()
    {
        // Game over if player falls below camera view
        float bottomOfCamera = Camera.main.transform.position.y - Camera.main.orthographicSize;

        if (player.position.y < bottomOfCamera)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        // Reload the current scene = restart game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}