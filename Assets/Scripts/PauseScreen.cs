using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{

    public string levelSelect;
    public string mainMenu;

    private LevelManager theLevelManager;

    public GameObject thePauseScreen;

    private PlayerController thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        theLevelManager = FindObjectOfType<LevelManager>();
        thePlayer = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //checks if pause button is pressed. 
        if (Input.GetButtonDown("Pause"))
        {
            //Checks if game is paused. If it is, then it unpauses the game. If not, then it pauses the game
            if (Time.timeScale == 0f)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        //Pauses the game

        //Freezes the game
        Time.timeScale = 0;

        //Activates pause screen, freezes the player, and pauses the music
        thePauseScreen.SetActive(true);
        thePlayer.canMove = false;
        theLevelManager.levelMusic.Pause();
    }

    //Unpauses the game
    public void ResumeGame()
    {
        Time.timeScale = 1f;

        thePauseScreen.SetActive(false);
        thePlayer.canMove = true;
        theLevelManager.levelMusic.Play();
    }

    public void LevelSelect()
    {
        //Saves the players current cheese count and lives
        PlayerPrefs.SetInt("CheeseCount", theLevelManager.cheeseCount);
        PlayerPrefs.SetInt("PlayerLives", theLevelManager.currentLives);

        //Unfreezes the game
        Time.timeScale = 1f;

        //Loads level select scene
        SceneManager.LoadScene(levelSelect);
    }

    public void QuitToMainMenu()
    {
        //Unfreezes the game
        Time.timeScale = 1f;
        //Loads main menu scene
        SceneManager.LoadScene(mainMenu);
    }
}
