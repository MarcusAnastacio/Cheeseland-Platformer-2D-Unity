using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{

    public int livesToGive;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        //If the player touches the object, this runs the AddLives function and deactivates the object
        if (other.tag == "Player")
        {
            theLevelManager.AddLives(livesToGive);
            gameObject.SetActive(false);
        }
    }

}
