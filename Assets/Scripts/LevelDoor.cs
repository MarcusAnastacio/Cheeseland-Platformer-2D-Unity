using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour
{

    public string levelToLoad;

    public bool unlocked;

    public Sprite doorBottomOpen;
    public Sprite doorTopOpen;
    public Sprite doorBottomClosed;
    public Sprite doorTopClosed;

    public SpriteRenderer doorTop;
    public SpriteRenderer doorBottom;

    // Start is called before the first frame update
    void Start()
    {
        //makes level 1 always be unlocked
        PlayerPrefs.SetInt("Level1", 1);

        //checks if the level is unlocked
        if (PlayerPrefs.GetInt(levelToLoad) == 1)
        {
            unlocked = true;
        }
        else
        {
            unlocked = false;
        }

        //if the level is unlocked, makes the door look open
        if (unlocked)
        {
            doorTop.sprite = doorTopOpen;
            doorBottom.sprite = doorBottomOpen;
        }
        //If the level is locked, makes the door look closed
        else
        {
            doorTop.sprite = doorTopClosed;
            doorBottom.sprite = doorBottomClosed; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //If the player presses jump by the open door, it will load the level
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("Jump") && unlocked)
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }
    }
}
