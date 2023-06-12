using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    private static readonly float BIRD_ROTATION_MODIFIER = 1F;

    // Cap the rotation angle of the bird at straight up or straight down so it doesn't look weird
    private static readonly int BIRD_ROTATION_MAX_ANGLE = 90;
    private static readonly int BIRD_ROTATION_MIN_ANGLE = -90;

    public Rigidbody2D myRigidbody;
    public Animator animator;

    private LogicScript logic;
    private SoundPlayerScript soundPlayer;

    public float flapStrength = 20;  // How high the bird flies up on a space press
    private bool isBirdAlive = true;
    private float birdTimeAlive = 0;  // How long the bird has been alive for (ie how long we've been playing)

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.Find("LogicManager").GetComponent<LogicScript>();
        soundPlayer = GameObject.Find("SoundManager").GetComponent<SoundPlayerScript>();

        birdTimeAlive = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SetBirdAngle();

        // Kill the bird if it flies too high or falls too low
        float birdHeight = myRigidbody.position.y;
        if (birdHeight > 16 || birdHeight < -16)
        {
            KillBird();
        }

        // If the bird is still alive, increment the timer tracking how long it's been going for.
        // We don't reset it to zero here because that would immediately slow the pipes down to their original speed.
        if (isBirdAlive)
        {
            birdTimeAlive += Time.deltaTime;
        }

        // Only allow the bird to fly up if it's alive
        if (isBirdAlive && Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.velocity = Vector2.up * flapStrength;
            animator.SetBool("isFlapping", true);
            soundPlayer.PlayFlapAudio();
        }
        else
        {
            animator.SetBool("isFlapping", false);
        }
    }

    public float getBirdTimeAlive()
    {
        return birdTimeAlive;
    }

    /**
     * The bird should rotate based on how fast it's moving vertically.
     * The faster it moves up, the more it turns up; The faster it moves down, the more it turn down.
     */
    private void SetBirdAngle()
    {
        float rotationAngle = myRigidbody.velocity.y * BIRD_ROTATION_MODIFIER;

        if (rotationAngle > BIRD_ROTATION_MAX_ANGLE) rotationAngle = BIRD_ROTATION_MAX_ANGLE;
        if (rotationAngle < BIRD_ROTATION_MIN_ANGLE) rotationAngle = BIRD_ROTATION_MIN_ANGLE;

        myRigidbody.SetRotation(rotationAngle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If anything collides with the bird, it means the game should end
        KillBird();
        soundPlayer.PlayHitAudio();
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
