using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    private static readonly float PIPE_INCREASING_SPAWN_INTERVAL_DAMPER = 0.1F;
    private static readonly float INITIAL_SPAWN_INTERVAL = 5F;

    private BirdScript birdScript;
    public GameObject pipe;

    private float spawnInterval = INITIAL_SPAWN_INTERVAL;
    private float timer = 0;
    private float maxAbsSpawnHeight = 10;  // The maximum vertical distance the pipe can spawn at from center

    // Start is called before the first frame update
    void Start()
    {
        birdScript = GameObject.Find("Bird").GetComponent<BirdScript>();

        spawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn a new pipe if we haven't in a while.
        // The longer the bird is alive, the more frequently we should spawn pipes.
        // We dampen the effect so the spawn interval doesn't shrink too quickly.
        if (birdScript.IsBirdAlive())
        {
            // Don't let the spawn interval be negative else bad things would happen.
            spawnInterval = Mathf.Max(0, INITIAL_SPAWN_INTERVAL - birdScript.getBirdTimeAlive() * PIPE_INCREASING_SPAWN_INTERVAL_DAMPER);
        }
        if (timer < spawnInterval)
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
