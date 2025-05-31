using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//lets the script access the UI
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public float waitToRespawn;
    public PlayerController thePlayer;

    public GameObject deathSplosion;

    public int cheeseCount;
    private int cheeseBonusLifeCount;

    public AudioSource cheeseSound;

    public Text cheeseText;

    public Image life1;
    public Image life2;
    public Image life3;

    public Sprite lifeFull;
    public Sprite lifeHalf;
    public Sprite lifeEmpty;

    public int maxHealth;
    public int healthCount;

    private bool respawning;

    public ResetOnRespawn[] objectsToReset;

    public bool invincible;

    public int currentLives;
    public int startingLives;
    public Text livesText;

    public GameObject gameOverScreen;

    public AudioSource levelMusic;
    public AudioSource gameOverMusic;

    public bool respawnCoActive;

    // Start is called before the first frame update
    void Start()
    {
        //makes reference to the player controller script
        thePlayer = FindObjectOfType<PlayerController>();

        //sets players health to the max health
        healthCount = maxHealth;

        //makes reference to the reset on respawn script
        objectsToReset = FindObjectsOfType<ResetOnRespawn>();

        //checks player prefs and updates our cheese count and lives
        if (PlayerPrefs.HasKey("CheeseCount"))
        {
            cheeseCount = PlayerPrefs.GetInt("CheeseCount");
        }

        if (PlayerPrefs.HasKey("PlayerLives"))
        {
            currentLives = PlayerPrefs.GetInt("PlayerLives");
        }
        else
        {
            currentLives = startingLives;
        }

        cheeseText.text = "Cheese: " + cheeseCount;

        livesText.text = "Lives x " + currentLives; 
    }

    // Update is called once per frame
    void Update()
    {
        //if players health reaches zero and they aren't already respawning, it runs the respawn funtion
        if (healthCount <= 0 && !respawning)
        {
            Respawn();
            respawning = true;
        }

        //If the player collects 100 cheeses, it gives them an extra life, then resets the bonus cheese count
        if (cheeseBonusLifeCount >= 100)
        {
            currentLives += 1;
            livesText.text = "Lives x " + currentLives;
            cheeseBonusLifeCount -= 100; 
        }
    }

    public void Respawn()
    {
        //safeguard that makes sure if the player is already respawning, it won't run this code a second time
        if (respawning)
        {
            return;
        }
        respawning = true;

        //decreases players lives by 1 and updates text
        currentLives -= 1;
        livesText.text = "Lives x " + currentLives;

        //checks if lives are more than 0. If so, the player respawns. If not, game over, player is deactivated, music stops, game over music plays.
        if (currentLives > 0)
        {
            //starts the respawn co Routine
            StartCoroutine("RespawnCo");
        }
        else
        {
            thePlayer.gameObject.SetActive(false);
            gameOverScreen.SetActive(true);
            levelMusic.Stop();
            gameOverMusic.Play();
        }
    }

    // Happens when the co Routine is started. Happens on a seperate timeline than the rest of the code.
    public IEnumerator RespawnCo()
    {
        respawnCoActive = true;
        //deactivates the player
        thePlayer.gameObject.SetActive(false);

        //plays our death explosion particle effect
        Instantiate(deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);

        //waits a bit before continuing
        yield return new WaitForSeconds(waitToRespawn);

        respawnCoActive = false;
        //resets the health count
        healthCount = maxHealth;
        //tells the code that the player is no longer respawning
        respawning = false;
        //starts the UpdateLifeMeter function
        UpdateLifeMeter();

        //resets the cheese count and updates the text
        cheeseCount = 0;
        cheeseText.text = "Cheese: " + cheeseCount;
        cheeseBonusLifeCount = 0;

        //respawns the player and reactivates it. 
        thePlayer.transform.position = thePlayer.respawnPosition;
        thePlayer.gameObject.SetActive(true);

        //For loop that goes through every object in the objectsToReset array and respawns it.
        for(int i = 0; i < objectsToReset.Length; i++)
        {
            objectsToReset[i].gameObject.SetActive(true);
            objectsToReset[i].ResetObject();
        }
        respawning = false;
    }

    //Happens when the player collects a cheese
    public void AddCheese(int cheeseToAdd)
    {
        //increases cheese count and bonus life cheese count 
        cheeseCount += cheeseToAdd;
        cheeseBonusLifeCount += cheeseToAdd;

        //updates text
        cheeseText.text = "Cheese: " + cheeseCount;
        //plays the cheese sound.
        cheeseSound.Play(); 
    }

    //happens when player is hurt
    public void HurtPlayer(int damageToTake)
    {
        //checks if player is invincible
        if (!invincible)
        {
            //lowers health and calls updatelifemeter function
            healthCount -= damageToTake;
            UpdateLifeMeter();

            //calls the knockback function
            thePlayer.Knockback();
            //plays the hurt sound
            thePlayer.hurtSound.Play();
        }
    }

    //happens when health pickup is collected
    public void GiveHealth(int healthToGive)
    {
        //increases health count
        healthCount += healthToGive; 

        //makes sure the health doesn't go over the max health
        if (healthCount > maxHealth)
        {
            healthCount = maxHealth;
        }

        //plays cheese sound
        cheeseSound.Play();
     
        UpdateLifeMeter();

    }

    public void UpdateLifeMeter()
    {
        //goes through  all 7 possible scenerios for healthCount and changes the UI sprite accordingly
        switch (healthCount)
        {
            case 6: 
                life1.sprite = lifeFull;
                life2.sprite = lifeFull;
                life3.sprite = lifeFull;
                return;

            case 5:
                life1.sprite = lifeFull;
                life2.sprite = lifeFull;
                life3.sprite = lifeHalf;
                return;

            case 4:
                life1.sprite = lifeFull;
                life2.sprite = lifeFull;
                life3.sprite = lifeEmpty;
                return;

            case 3:
                life1.sprite = lifeFull;
                life2.sprite = lifeHalf;
                life3.sprite = lifeEmpty;
                return;
            case 2:
                life1.sprite = lifeFull;
                life2.sprite = lifeEmpty;
                life3.sprite = lifeEmpty;
                return;
            case 1:
                life1.sprite = lifeHalf;
                life2.sprite = lifeEmpty;
                life3.sprite = lifeEmpty;
                return;
            case 0:
                life1.sprite = lifeEmpty;
                life2.sprite = lifeEmpty;
                life3.sprite = lifeEmpty;
                return;

                default:
                life1.sprite = lifeEmpty;
                life2.sprite = lifeEmpty;
                life3.sprite = lifeEmpty;
                return;

        }

    }

    public void AddLives(int livesToAdd)
    {
        cheeseSound.Play();

        //adds live(s) and updates the text
        currentLives += livesToAdd;
        livesText.text = "Lives x " + currentLives;

    }

}
