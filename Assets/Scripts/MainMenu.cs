using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Initialize variables
    public string firstLevel;
    public string levelSelect;

    public string[] levelNames;

    public int startingLives;

    public AudioSource mainMenuMusic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Happens when player presses New Game button
    public void NewGame()
    {
        //resets player prefs in order to relock all the levels 
        for (int i = 0; i < levelNames.Length; i++)
        {
            PlayerPrefs.SetInt(levelNames[i], 0);
        }
        //Stops the main menu music
        mainMenuMusic.Stop();
        //resets the players cheese count and lives
        PlayerPrefs.SetInt("CheeseCount", 0);
        PlayerPrefs.SetInt("PlayerLives", startingLives);
        //loads the first level
        SceneManager.LoadScene(firstLevel);
    }
    
    //loads the level select scene when the level select button is pressed
    public void Continue()
    {
        //Stops the main menu music
        mainMenuMusic.Stop();
        SceneManager.LoadScene(levelSelect);
    }

    //Quits the game if the quit button is pressed
    public void QuitGame() 
    {
        Application.Quit();
    }
}
