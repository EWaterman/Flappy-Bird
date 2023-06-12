using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    private static readonly float INITIAL_MOVE_SPEED = 5F;
    private static readonly float PIPE_INCREASING_MOVE_SPEED_DAMPER = 0.5F;

    private BirdScript birdScript;

    private float moveSpeed = INITIAL_MOVE_SPEED;
    private float deadZone = -40;

    // Start is called before the first frame update
    void Start()
    {
        birdScript = GameObject.Find("Bird").GetComponent<BirdScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // The longer the bird is alive, the faster we should move the pipes.
        // We dampen the effect so the movement doesn't increase too quickly.
        if (birdScript.IsBirdAlive())
        {
            moveSpeed = Mathf.Max(moveSpeed, INITIAL_MOVE_SPEED + (birdScript.getBirdTimeAlive() * PIPE_INCREASING_MOVE_SPEED_DAMPER));
        }
        Debug.Log("moveSpeed: " + moveSpeed);

        // If the pipe hasn't left the screen yet, move it to the left
        if (transform.position.x >= deadZone)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        // Otherwise despawn the pipe
        else
        {
            Destroy(gameObject);
        }
    }
}
