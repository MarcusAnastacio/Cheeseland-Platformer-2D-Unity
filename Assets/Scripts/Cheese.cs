using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour
{

    public LevelManager theLevelManager;

    public int cheeseValue;

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
        //if the player touches the cheese, it runs the AddCheese function and deactivates the cheese
        if(other.tag == "Player")
        {
            theLevelManager.AddCheese(cheeseValue);

            //Destroy(gameObject);

            gameObject.SetActive(false);
        }
    }
}
