using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;

    public float bounceForce;

    public GameObject deathSplosion;


    // Start is called before the first frame update
    void Start()
    {
        //Gets the component of the players rigidbody
        playerRigidbody = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //If the stomp box touches an enemy
        if (other.tag == "Enemy")
        {
            //Destroy(other.gameObject);

            //deactivates the enemy
            other.gameObject.SetActive(false);
            //makes the deathSplosion particle effect
            Instantiate(deathSplosion, other.transform.position, other.transform.rotation);
            //makes the player bounce off of the enemy
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, bounceForce, 0f);
        }
        //If the stomp box touches the boss
        if (other.tag == "Boss")
        {
            //makes the player bounce off of the boss
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, bounceForce, 0f);
            //makes the boss take damage
            other.transform.parent.GetComponent<Boss>().takeDamage = true;
        }
    }
}
