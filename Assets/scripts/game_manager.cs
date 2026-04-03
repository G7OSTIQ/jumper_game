using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_manager : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject movingPlatformPrefab;
    public GameObject enemyPrefab;
    public Transform player;
    public int enemyEveryNPlatforms = 5;

    private float nextSpawnY = 1f;
    private int platformIndex = 0;
    private float spawnAheadDistance = 30f;  // how far above camera to keep spawning
    private float despawnBelowDistance = 20f; // how far below camera to destroy

    private List<GameObject> activePlatforms = new List<GameObject>();

    void Start()
    {
        // spawn initial batch of platforms to fill the screen
        while (nextSpawnY < spawnAheadDistance)
        {
            SpawnPlatformRow();
        }
    }

    void Update()
    {
        float cameraTop = Camera.main.transform.position.y + Camera.main.orthographicSize;
        float cameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;

        // keep spawning platforms above camera
        while (nextSpawnY < cameraTop + spawnAheadDistance)
        {
            SpawnPlatformRow();
        }

        // destroy platforms too far below camera
        for (int i = activePlatforms.Count - 1; i >= 0; i--)
        {
            if (activePlatforms[i] == null)
            {
                activePlatforms.RemoveAt(i);
                continue;
            }

            if (activePlatforms[i].transform.position.y < cameraBottom - despawnBelowDistance)
            {
                Destroy(activePlatforms[i]);
                activePlatforms.RemoveAt(i);
            }
        }

        // game over if player falls below camera
        float bottomOfCamera = cameraBottom;
        if (player.position.y < bottomOfCamera)
        {
            GameOver();
        }
    }

    void SpawnPlatformRow()
    {
        nextSpawnY += Random.Range(1.2f, 2f);
        float spawnX = Random.Range(-4f, 4f);
        Vector3 spawnPosition = new Vector3(spawnX, nextSpawnY, 0f);

        GameObject platform;

        // every 4th platform is moving
        if (platformIndex > 5 && platformIndex % 4 == 0)
        {
            platform = Instantiate(movingPlatformPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }

        activePlatforms.Add(platform);

        // spawn enemy on normal platforms only
        if (platformIndex > 5 && platformIndex % enemyEveryNPlatforms == 0 && platformIndex % 4 != 0)
        {
            Vector3 enemyPos = new Vector3(spawnX, nextSpawnY + 0.5f, 0f);
            GameObject enemy = Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
            enemy.transform.SetParent(platform.transform);
        }

        // spawn extra platform nearby
        if (platformIndex % 3 == 0)
        {
            Vector3 extraPos = new Vector3(
                Random.Range(-4f, 4f),
                nextSpawnY + Random.Range(-0.3f, 0.3f),
                0f
            );

            // make sure it's not too close to the main one
            if (Mathf.Abs(extraPos.x - spawnX) > 1.5f)
            {
                GameObject extra = Instantiate(platformPrefab, extraPos, Quaternion.identity);
                activePlatforms.Add(extra);
            }
        }

        platformIndex++;
    }

    void GameOver()
    {
        game_over_manager.instance.ShowGameOver();
    }
}