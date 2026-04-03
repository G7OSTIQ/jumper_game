using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_manager : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject enemyPrefab;
    public int platformCount = 300;
    public Transform player;
    public int enemyEveryNPlatforms = 5;

    void Start()
    {
        Vector3 spawnPosition = new Vector3(0f, 1f, 0f);
        List<Vector3> spawnedPositions = new List<Vector3>();

        for (int i = 0; i < platformCount; i++)
        {
            spawnPosition.y += Random.Range(1.2f, 2f);
            spawnPosition.x = Random.Range(-4f, 4f);

            GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            spawnedPositions.Add(spawnPosition);

            // spawn enemy
            if (i > 5 && i % enemyEveryNPlatforms == 0)
            {
                Vector3 enemyPos = new Vector3(spawnPosition.x, spawnPosition.y + 0.5f, 0f);
                GameObject enemy = Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
                enemy.transform.SetParent(platform.transform);
            }

            // spawn extra platform
            if (i % 3 == 0)
            {
                Vector3 extraPos = new Vector3(
                    Random.Range(-4f, 4f),
                    spawnPosition.y + Random.Range(-0.3f, 0.3f),
                    0f
                );

                bool tooClose = false;
                foreach (Vector3 pos in spawnedPositions)
                {
                    if (Vector3.Distance(extraPos, pos) < 1.5f)
                    {
                        tooClose = true;
                        break;
                    }
                }

                if (!tooClose)
                {
                    Instantiate(platformPrefab, extraPos, Quaternion.identity);
                    spawnedPositions.Add(extraPos);
                }
            }
        }
    }

    void Update()
    {
        float bottomOfCamera = Camera.main.transform.position.y - Camera.main.orthographicSize;
        if (player.position.y < bottomOfCamera)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        game_over_manager.instance.ShowGameOver();
    }
}