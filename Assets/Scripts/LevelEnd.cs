using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Lets the script access the scenes
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{

    public string levelToLoad;
    public string levelToUnlock;

    private PlayerController thePlayer;
    private CameraController theCamera;
    private LevelManager theLevelManager;

    public float waitToMove;
    public float waitToLoad;

    private bool movePlayer;

    public Sprite flagOpen;

    private SpriteRenderer theSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //makes reference to other scripts
        thePlayer = FindObjectOfType<PlayerController>();
        theCamera = FindObjectOfType<CameraController>();
        theLevelManager = FindObjectOfType<LevelManager>();

        //makes reference to the sprite renderer so it can be used
        theSpriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //makes player continously move to the right if movePlayer is true
        if (movePlayer)
        {
            thePlayer.myRigidbody.velocity = new Vector3(thePlayer.moveSpeed, thePlayer.myRigidbody.velocity.y, 0f);
        }
    }

    //Happens when something with a box collider enters another object with a box collider set as a trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        //checks if the other object has the tag player
        if (other.tag == "Player")
        {
            //Opens the flag
            theSpriteRenderer.sprite = flagOpen; 

            //starts the Level end coroutine
            StartCoroutine("LevelEndCo");
        }
    }

    public IEnumerator LevelEndCo()
    {
        //makes player unable to move and invincible. Freezes the camera.
        thePlayer.canMove = false;
        theCamera.followTarget = false;
        theLevelManager.invincible = true;

        //stops the music and plays the game over music
        theLevelManager.levelMusic.Stop();
        theLevelManager.gameOverMusic.Play();

        //makes the velocity of the player zero
        thePlayer.myRigidbody.velocity = Vector3.zero;

        //Updates the player prefs
        PlayerPrefs.SetInt("CheeseCount", theLevelManager.cheeseCount);
        PlayerPrefs.SetInt("PlayerLives", theLevelManager.currentLives);

        //unlocks the next level
        PlayerPrefs.SetInt(levelToUnlock, 1);

        //waits a bit before letting the player move
        yield return new WaitForSeconds(waitToMove);
        //makes player able to move again
        movePlayer = true;
        //waits abit before loading the next level
        yield return new WaitForSeconds(waitToLoad);
        //loads the next level
        SceneManager.LoadScene(levelToLoad);
    }
}
