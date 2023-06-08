using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public LogicScript logic;
    public Animation animator;

    public float flapStrength = 20;  // How high the bird flies up on a space press
    private bool isBirdAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.Find("LogicManager").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Kill the bird if it flies too high or falls too low
        float birdHeight = myRigidbody.position.y;
        if (birdHeight > 16 || birdHeight < -16)
        {
            KillBird();
        }

        // Only allow the bird to fly up if it's alive
        if (isBirdAlive && Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.velocity = Vector2.up * flapStrength;
            //animator.SetBool("flapping", true);
        }
        else
        {
            //animator.SetBool("flapping", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If anything collides with the bird, it means the game should end
        KillBird();
    }

    /**
     * To be called whenever the bird should die (RIP)
     */
    private void KillBird()
    {
        isBirdAlive = false;
        logic.GameOver();
    }

    public bool IsBirdAlive()
    {
        return isBirdAlive;
    }
}
