using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEffect : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitDuration;
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyUp(KeyCode.S))
        {
            waitDuration = 0.01f;
        }

        if(Input.GetKey(KeyCode.S))
        {
            if (waitDuration <= 0)
            {
                effector.rotationalOffset = 180f;
                waitDuration = 0.01f;
            }
            else
            {
                waitDuration -= Time.deltaTime;
            }
        }
        */
        

        if(waitDuration >=0)
        {
            effector.rotationalOffset = 180f;
            waitDuration -= Time.deltaTime;
        }
        else
        {
            effector.rotationalOffset = 0;
        }
        
        


        if(Input.GetKeyDown(KeyCode.S))
        {
            waitDuration += 0.5f;
        }
    }
}