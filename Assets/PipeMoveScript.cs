using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadZone = -40;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
