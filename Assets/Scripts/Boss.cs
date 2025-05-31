using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public bool bossActive;

    public float timeBetweenDrops;
    private float timeBetweenDropsStore;
    private float dropCount;

    public float waitForPlatforms;
    private float platformCount;

    public Transform leftPoint;
    public Transform rightPoint;
    public Transform dropSawSpawnPoint;

    public GameObject dropSaw;

    public GameObject theBoss;
    public bool bossRight;

    public GameObject rightPlatforms;
    public GameObject leftPlatforms;

    public bool takeDamage;

    public int startingHealth;
    private int currentHealth;

    public GameObject levelExit;

    private CameraController theCamera;

    private LevelManager theLevelManager;

    public bool waitingForRespawn;

    public AudioSource bossLevelMusic;

    // Start is called before the first frame update
    void Start()
    {
        //sets the starting values for a bunch of variables
        timeBetweenDropsStore = timeBetweenDrops;
        dropCount = timeBetweenDrops;
        platformCount = waitForPlatforms;
        currentHealth = startingHealth;
        
        theCamera = FindObjectOfType<CameraController>();

        //makes the boss start on the right side
        theBoss.transform.position = rightPoint.position;
        bossRight = true;

        theLevelManager = FindObjectOfType<LevelManager>();

        //makes it so that the boss music isn't playing over the level music
        //bossLevelMusic.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        //checks if the rspawn co routine is active
        if (theLevelManager.respawnCoActive)
        {
            //deactivates the boss battle
            bossActive = false;
            waitingForRespawn = true;
  
        }

        //checks if a respawn has happen and if the respawn co routine is no longer active
        if (waitingForRespawn && !theLevelManager.respawnCoActive)
        {
            //deactivates the boss and platforms
            theBoss.SetActive(false);
            leftPlatforms.SetActive(false);
            rightPlatforms.SetActive(false);

            //resets everything in the boss battle
            timeBetweenDrops = timeBetweenDropsStore;

            platformCount = waitForPlatforms;
            dropCount = timeBetweenDrops;

            theBoss.transform.position = rightPoint.position;
            bossRight = true;

            currentHealth = startingHealth;

            //camera is now back to following the player
            theCamera.followTarget = true;

            waitingForRespawn = false;
        }

        //checks if the boss fight is active
        if (bossActive)
        {

            //moves camera to middle
            theCamera.followTarget = false;
            theCamera.transform.position = Vector3.Lerp(theCamera.transform.position, new Vector3(transform.position.x, theCamera.transform.position.y, theCamera.transform.position.z), theCamera.smoothing * Time.deltaTime);

            theBoss.SetActive(true);

            //starts counting down the time between saw drops
            if (dropCount > 0)
            {
                dropCount -= Time.deltaTime;
            }
            else
            {
                //randomly spawns a falling saw
                dropSawSpawnPoint.position = new Vector3(Random.Range(leftPoint.position.x, rightPoint.position.x), dropSawSpawnPoint.position.y, dropSawSpawnPoint.position.z);
                Instantiate(dropSaw, dropSawSpawnPoint.position, dropSawSpawnPoint.rotation);
                dropCount = timeBetweenDrops;
            }

            if (bossRight)
            {
                //counts down for the platform spawn
                if (platformCount > 0)
                {
                    platformCount -= Time.deltaTime;
                }
                //spawns the right platforms
                else
                {
                    rightPlatforms.SetActive(true);
                }
            }
            else
            {
                if (platformCount > 0)
                {
                    platformCount -= Time.deltaTime;
                }
                //spawns the left platforms
                else
                {
                    leftPlatforms.SetActive(true);
                }
            }

            //happens when boss takes damage
            if (takeDamage)
            {
                //lowers boss' health
                currentHealth -= 1;

                //checks if bosses health is zero
                if (currentHealth <= 0)
                {
                    //activates the level exit and deactivates the boss fight
                    levelExit.SetActive(true);

                    theCamera.followTarget = true;

                    gameObject.SetActive(false);
                    bossLevelMusic.Stop();
                    theLevelManager.levelMusic.Play();
                }

                //moves boss to other side
                if (bossRight)
                {
                    theBoss.transform.position = leftPoint.position;
                }
                else
                {
                    theBoss.transform.position = rightPoint.position;
                }
                bossRight = !bossRight;
                //deactivates platforms
                rightPlatforms.SetActive(false);
                leftPlatforms.SetActive(false);
                //resets platform countdown
                platformCount = waitForPlatforms;
                //halves the time between saw drop 
                timeBetweenDrops = timeBetweenDrops / 2f;

                takeDamage = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //starts boss battle once the player enters the zone.
        if (other.tag == "Player" && !bossActive)
        {
            bossActive = true;
            //stops the music and plays the boss music
            theLevelManager.levelMusic.Stop();
            bossLevelMusic.Play();
        }
    }
}
