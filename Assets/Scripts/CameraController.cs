using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject target;
    public float followAhead;

    private Vector3 targetPosition;

    public float smoothing;

    public bool followTarget;

    // Start is called before the first frame update
    void Start()
    {
        //sets the camera to be following it's target
        followTarget = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (followTarget)
        {
            //moves camera to the same x position as the target
            targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);

            //this moves the target of the camera ahead of the player
            if (target.transform.localScale.x > 0f)
            {
                targetPosition = new Vector3(targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
            }
            else
            {
                targetPosition = new Vector3(targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
            }

            //makes the cameras movements more smooth
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
    }
}
