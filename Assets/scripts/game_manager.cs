using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_manager : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject movingPlatformPrefab;
    public GameObject enemyPrefab;
    public GameObject jellyfishPrefab;     // ✅ drag jellyfish prefab in Inspector
    public Transform player;
    public int enemyEveryNPlatforms = 5;
    public int jellyfishEveryNPlatforms = 15; // ✅ tune how often jellyfish spawn

    private float nextSpawnY = 1f;
    private int platformIndex = 0;
    private float spawnAheadDistance = 30f;
    private float despawnBelowDistance = 20f;

    private List<GameObject> activePlatforms = new List<GameObject>();
    private List<GameObject> activeJellyfish = new List<GameObject>(); // ✅ track jellyfish

    void Start()
    {
        while (nextSpawnY < spawnAheadDistance)
        {
            SpawnPlatformRow();
        }
    }

    void Update()
    {
        float cameraTop = Camera.main.transform.position.y + Camera.main.orthographicSize;
        float cameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;

        while (nextSpawnY < cameraTop + spawnAheadDistance)
        {
            SpawnPlatformRow();
        }

        // destroy platforms too far below camera
        for (int i = activePlatforms.Count - 1; i >= 0; i--)
        {
            if (activePlatforms[i] == null) { activePlatforms.RemoveAt(i); continue; }
            if (activePlatforms[i].transform.position.y < cameraBottom - despawnBelowDistance)
            {
                Destroy(activePlatforms[i]);
                activePlatforms.RemoveAt(i);
            }
        }

        // ✅ destroy jellyfish too far below camera
        for (int i = activeJellyfish.Count - 1; i >= 0; i--)
        {
            if (activeJellyfish[i] == null) { activeJellyfish.RemoveAt(i); continue; }
            if (activeJellyfish[i].transform.position.y < cameraBottom - despawnBelowDistance)
            {
                Destroy(activeJellyfish[i]);
                activeJellyfish.RemoveAt(i);
            }
        }

        if (player.position.y < cameraBottom)
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

        if (platformIndex > 5 && platformIndex % 4 == 0)
        {
            platform = Instantiate(movingPlatformPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }

        activePlatforms.Add(platform);

        if (platformIndex > 5 && platformIndex % enemyEveryNPlatforms == 0 && platformIndex % 4 != 0)
        {
            Vector3 enemyPos = new Vector3(spawnX, nextSpawnY + 0.5f, 0f);
            GameObject enemy = Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
            enemy.transform.SetParent(platform.transform);
        }

        // ✅ spawn jellyfish between platforms, not on them
        if (platformIndex > 8 && platformIndex % jellyfishEveryNPlatforms == 0)
        {
            Vector3 jellyPos = new Vector3(
                Random.Range(-3f, 3f),
                nextSpawnY + Random.Range(1f, 2f), // float above platform
                0f
            );
            GameObject jelly = Instantiate(jellyfishPrefab, jellyPos, Quaternion.identity);
            activeJellyfish.Add(jelly); // ✅ track it for cleanup
        }

        if (platformIndex % 3 == 0)
        {
            Vector3 extraPos = new Vector3(
                Random.Range(-4f, 4f),
                nextSpawnY + Random.Range(-0.3f, 0.3f),
                0f
            );

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