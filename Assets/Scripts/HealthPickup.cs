using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public int healthToGive;

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
        //If the player touches the pick up, this runs the GiveHealth function and deactivates the pickup
        if (other.tag == "Player")
        {
            theLevelManager.GiveHealth(healthToGive);
            gameObject.SetActive(false);
        }
    }
}
