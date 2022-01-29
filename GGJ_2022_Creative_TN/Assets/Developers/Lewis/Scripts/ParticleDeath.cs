using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDeath : MonoBehaviour
{
    private ParticleSystem part;
    private List<ParticleCollisionEvent> CollisionEvents = new List<ParticleCollisionEvent>();
    public Transform respawn;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollision = part.GetCollisionEvents(other, CollisionEvents);
        int i = 0;
        if(i < numCollision)
        {
            other.transform.position = respawn.transform.position;
        }
            
    }
}
