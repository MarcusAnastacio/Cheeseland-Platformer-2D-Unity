using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlip : MonoBehaviour
{

    private SpriteRenderer theSpriteRenderer;

    private Boss theBoss;

    // Start is called before the first frame update
    void Start()
    {
        //makes reference to the sprite renderer so it can be used
        theSpriteRenderer = GetComponent<SpriteRenderer>();

        theBoss = FindObjectOfType<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        //checks if the boss fight is active
        if (theBoss.bossActive)
        {
            //Checks if the boss is right or not
            if (theBoss.bossRight)
            {
                //makes the boss face to the left
                theSpriteRenderer.flipX = true;
            }
            else
            {
                //makes the boss face to the right
                theSpriteRenderer.flipX = false;
            }
        }

    }
}
