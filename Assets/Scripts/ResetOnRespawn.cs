using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnRespawn : MonoBehaviour
{

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 startLocalScale;

    private Rigidbody2D myRigidbody; 

    // Start is called before the first frame update
    void Start()
    {
        //Saves all of the ovjects starting transform values
        startPosition = transform.position;
        startRotation = transform.rotation;
        startLocalScale = transform.localScale;

        //checks if there is a Rigidbody attached to the object
        if(GetComponent<Rigidbody2D>() != null)
        {
            myRigidbody = GetComponent<Rigidbody2D>();
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetObject()
    {
        //Sets all the objects to be back to their starting transforms
        transform.position = startPosition;
        transform.rotation = startRotation;
        transform.localScale = startLocalScale;
        
        //checks if a rigidbody was really found and resets the velocity
        if(myRigidbody != null)
        {
            myRigidbody.velocity = Vector3.zero;
        }
    }
}
