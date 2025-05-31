using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    public GameObject objectToMove;

    public Transform startPoint;
    public Transform endPoint;

    public float moveSpeed;

    private Vector3 currentTarget; 

    // Start is called before the first frame update
    void Start()
    {
        //makes the target the end point
        currentTarget = endPoint.position;  
    }

    // Update is called once per frame
    void Update()
    {
        //makes the object move towards the current target
        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, currentTarget, moveSpeed * Time.deltaTime);

        //If the object reaches the end or start point, this changes the target to be the opposite point
        if(objectToMove.transform.position == endPoint.position)
        {
            currentTarget = startPoint.position; 
        }

        if(objectToMove.transform.position == startPoint.position)
        {
            currentTarget = endPoint.position; 
        }
    }
}
