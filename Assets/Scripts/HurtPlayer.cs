using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{

    private LevelManager theLevelManager;

    public int damageToGive;

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
        //If the player contacts with an enemy or danger hitbox, this runs the HurtPlayer function
        if(other.tag == "Player")
        {

            theLevelManager.HurtPlayer(damageToGive);
        }
    }
}
