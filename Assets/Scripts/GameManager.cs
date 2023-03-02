using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static float MAX_VELOCITY = -4f, GAME_OVER_Y = -7;


    private void Awake()
    {
        instance = this;
    }

    public List<GameObject> blocks;

    public float spawnYValue, spawnXRange;

    public float minSpawnTime = 3, maxSpawnTime = 4;

    public float timeScalePerSecond = 0.0025f;

    public TextMeshProUGUI gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnBlocks());
    }

    IEnumerator SpawnBlocks()
    {
        while (true)
        {
            GameObject b = Instantiate(blocks[Random.Range(0, blocks.Count)], new Vector3(Random.Range(-spawnXRange, spawnXRange), spawnYValue, 0), Quaternion.identity);
            if (Random.value > 0.5f) b.transform.Rotate(0, 0, 90f);

            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime) * (1 - Time.time * timeScalePerSecond)); 
        }
    }

    public void OnGameOver()
    {
        gameOverText.text = "GAME OVER :( YOU GOT " + PlateController.instance.attachedBlocks.Count + " BLOCKS";

        Time.timeScale = 0;
    }

}
