using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{

    public float moveSpeed;
    private bool canMove;

    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        { 
            //moves the spider enemy if canMove is true
            myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
        }

    }

    //happens when object enters the cameras view
    void OnBecameVisible()
    {
        //makes the spider able to move
        canMove = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //deactivates the object if it touches the kill plane
        if (other.tag == "KillPlane")
        {
            //Destroy(gameObject);

            gameObject.SetActive(false);
        }
    }    

    //happens whenever an object is enabled/set active
    void OnEnable()
    {
        //makes it so the spider doesn't move while the player can't see it
        canMove = false; 
    }
}
