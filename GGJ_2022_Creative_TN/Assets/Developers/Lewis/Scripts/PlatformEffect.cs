using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEffect : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float waitDuration;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {  
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
            waitDuration += 0.6f;
        }
    }
}
