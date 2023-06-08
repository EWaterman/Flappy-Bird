using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMiddleScript : MonoBehaviour
{
    private static readonly int SCORE_FOR_ENTERING_PIPE = 1;

    private LogicScript logic;
    private GameObject bird;
    private BirdScript birdScript;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.Find("LogicManager").GetComponent<LogicScript>();
        bird = GameObject.Find("Bird");
        birdScript = bird.GetComponent<BirdScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the bird enters a pipe while alive, increment the score
        if (collision.gameObject == bird && birdScript.IsBirdAlive())
        {
            logic.AddScore(SCORE_FOR_ENTERING_PIPE);
        }
    }
}
