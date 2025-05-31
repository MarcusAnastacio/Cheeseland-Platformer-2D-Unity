using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private float activeMoveSpeed;

    public bool canMove;

    public Rigidbody2D myRigidbody;

    public float jumpSpeed;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public bool isGrounded;

    private Animator myAnim;

    public Vector3 respawnPosition;

    public LevelManager theLevelManager;

    public GameObject stompBox;

    public float knockbackForce;
    public float knockbackLength;
    private float knockbackCounter;

    public float invincibilityLength;
    private float invincibilityCounter;

    public AudioSource jumpSound;
    public AudioSource hurtSound;

    private bool onPlatform;
    public float onPlatformSpeedModifier;

    // Start is called before the first frame update
    void Start()
    {
        //gets all the components necessary to run the code
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        //sets the respawn position to be the players starting position
        respawnPosition = transform.position;

        theLevelManager = FindObjectOfType<LevelManager>();

        //makes the active move speed equal to the move speed
        activeMoveSpeed = moveSpeed;
        //makes the player able to move
        canMove = true;
    }

    // Update is called once per frame
    void Update() {

        //checks if the player is touching the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        //checks if the player is not being knocked back and if canMove.
        if (knockbackCounter <= 0 && canMove)
        {
            //modifies our speed to be normal while on a moving platform
            if (onPlatform)
            {
                activeMoveSpeed = moveSpeed * onPlatformSpeedModifier;
            }
            else
            {
                activeMoveSpeed = moveSpeed; 
            }

            //moves player to the right if right input is pressed
            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                myRigidbody.velocity = new Vector3(activeMoveSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            //moves player to the left if left input is pressed
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                myRigidbody.velocity = new Vector3(-activeMoveSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            //if there is no input, makes the player not move
            else
            {
                myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);
            }

            //If jump button is pressed while player is touching the ground, makes the player jump.
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);
                jumpSound.Play();
            }
 
        }

        //checks if the player has been knocked back
        if(knockbackCounter > 0)
        {
            //makes our knockback counter count down for each frame that passes
            knockbackCounter -= Time.deltaTime;



            //checks what direction the player is facing and makes the knockback be the opposite of that direction.
            if(transform.localScale.x > 0)
            { 
            //sends the player flying back diagnoly to the left
            myRigidbody.velocity = new Vector3(-knockbackForce, knockbackForce, 0f);
            }
            else
            {
                //sends the player flying back diagnoly to the right
                myRigidbody.velocity = new Vector3(knockbackForce, knockbackForce, 0f);
            }
        }

        //makes invincibilty count down
        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime; 
        }

        //turns off invincibility  when count down reaches zero or less
        if(invincibilityCounter <= 0)
        {
            theLevelManager.invincible = false;
        }

        //sets some variables for the animator
        myAnim.SetFloat("Speed", Mathf.Abs(myRigidbody.velocity.x));
        myAnim.SetBool("Grounded", isGrounded);

        //sets the stompBox to be active only while the player is falling in the air
        if(myRigidbody.velocity.y < 0)
        {
            stompBox.SetActive(true);
        }
        else
        {
            stompBox.SetActive(false);
        }
    }

    public void Knockback()
    {
        //sets the knockback counter
        knockbackCounter = knockbackLength;
        //sets the invincibility counter
        invincibilityCounter = invincibilityLength;
        //makes player invincible for a little bit during and after knockback
        theLevelManager.invincible = true; 
    }

    //happens when the player's box collider collides with another box colider set as a trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        //happens if the player collides with the kill plane
        if (other.tag == "KillPlane")
        {
            
            //gameObject.SetActive(false);

            //transform.position = respawnPosition;

            Debug.Log("Entered the kill plane!");

            //runs the respawn function found in the Level Manager script
            theLevelManager.Respawn(); 
        }

        //happens if the player collides with a checkpoint
        if (other.tag == "Checkpoint")
        {
            //sets the players respawn position to the checkpoint
            respawnPosition = other.transform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //If the player is standing on the moving platform, it makes the player a child of the moving platform
        if(other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
            onPlatform = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        //When the player leaves the moving platform, this makes the player no longer be a child of the moving platform
        if(other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
            onPlatform = false; 
        }
    }
}
