using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{

    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //counts down the life time value
        lifeTime = lifeTime - Time.deltaTime;
        
        //destroys the object if the life time reaches zero
        if(lifeTime <= 0f)
        {
            Destroy(gameObject);
        }

    }
}
