using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Declare our singleton instance
    public static GameManager instance;

    // The maximum downwards velocity that blocks will be able to reach
    public static float MAX_VELOCITY = -4f;

    // The Y value that a block must reach in order for the game to end
    public static float GAME_OVER_Y = -7;

    // Awake is called before Start, so it's the ideal place to set singletons
    private void Awake()
    {
        // Set our singleton instances
        instance = this;
    }

    // The list of all blocks that can be spawned
    public List<GameObject> blockPrefabs;

    // The Y value at which blocks will be spawned
    public float spawnYValue;

    // The distance from 0 on the X axis that blocks may be spawned at
    public float spawnXRange;

    // The minimum and maximum gaps between block spawns
    public float minSpawnTime = 3, maxSpawnTime = 4;

    // The rate per second at which the block spawning speed increases
    public float timeScalePerSecond = 0.0025f;

    // The text object that we will use to notify the player of a game over
    public TextMeshProUGUI gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        // Start the infinite block spawning coroutine
        StartCoroutine(SpawnBlocks());
    }

    IEnumerator SpawnBlocks()
    {
        while (true)
        {
            // Instantiate a randomly selected block prefab
            GameObject b = Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Count)], new Vector3(Random.Range(-spawnXRange, spawnXRange), spawnYValue, 0), Quaternion.identity);

            // 50% chance to rotate the spawned block, for some variety
            if (Random.value > 0.5f) b.transform.Rotate(0, 0, 90f);

            // Wait a random amount of time between the minimum and maximum spawn gaps before spawning the next block
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime) * (1 - Time.time * timeScalePerSecond)); 
        }
    }

    // Called in the Block script once a block falls off the screen
    public void OnGameOver()
    {
        // Set the game over text
        gameOverText.text = "GAME OVER :( YOU GOT " + PlatformController.instance.attachedBlocks.Count + " BLOCKS";

        // Pause the game
        Time.timeScale = 0;
    }

}
