using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private ParticleSystem test;
    private List<ParticleCollisionEvent> CollisionEvents = new List<ParticleCollisionEvent>();

    private void Start()
    {
        test = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollision = test.GetCollisionEvents(other, CollisionEvents);
        int i = 0;
        if (i < numCollision)
        {
            other.transform.position = new Vector3(0, 0, 0);
        }
    }
}
