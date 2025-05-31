using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public string levelSelect;
    public string mainMenu;

    private LevelManager theLevelManager;

    // Start is called before the first frame update
    void Start()
    {
        theLevelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        //Resets the cheese count and lives of the player
        PlayerPrefs.SetInt("CheeseCount", 0);
        PlayerPrefs.SetInt("PlayerLives", theLevelManager.startingLives);

        //Reloads the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelSelect()
    {
        //Resets the cheese count and lives of the player
        PlayerPrefs.SetInt("CheeseCount", 0);
        PlayerPrefs.SetInt("PlayerLives", theLevelManager.startingLives);

        //Loads the level select scene
        SceneManager.LoadScene(levelSelect);
    }

    //loads the main menu scene
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
