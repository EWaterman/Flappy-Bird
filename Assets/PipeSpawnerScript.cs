using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRate = 5;
    public float timer = 0;
    public float maxAbsSpawnHeight = 10;  // The maximum vertical distance the pipe can spawn at from center

    // Start is called before the first frame update
    void Start()
    {
        spawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        // Every X seconds, spawn a new pipe
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnPipe();
            timer = 0;
        }
    }

    void spawnPipe()
    {
        // Vary the vertical height of each pipe within some max and min bounds
        float minSpawnHeight = transform.position.y - maxAbsSpawnHeight;
        float maxSpawnHeight = transform.position.y + maxAbsSpawnHeight;
        float actualSpawnHeight = Random.Range(minSpawnHeight, maxSpawnHeight);

        Instantiate(pipe, new Vector3(transform.position.x, actualSpawnHeight), transform.rotation);
    }
}
