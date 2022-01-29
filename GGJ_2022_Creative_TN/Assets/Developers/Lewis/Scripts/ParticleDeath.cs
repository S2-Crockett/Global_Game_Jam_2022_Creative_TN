using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDeath : MonoBehaviour
{
    public bool hit = false;
    public float timer = 0;
    public PhysicsMaterial2D phyMat;

    void Start()
    {
        
    }

    private void Update()
    {
        if(hit)
        {
            timer -= Time.deltaTime;
            phyMat.bounciness = 1;
        }
        if(timer < 0)
        {
            hit = false; ;
            phyMat.bounciness = 0;
            timer = 0f;
        }
        
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            if(timer >= 0)
            {
                phyMat.bounciness = 1;
                timer = 0.5f;
                hit = true;
            }
            /*
            if(timer >= 0)
            {
                print("ENTERED");
                //rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
                //rb.velocity = new Vector2(-_moveInput * 100, rb.velocity.y);
                rb.AddForce(-rb.velocity, ForceMode2D.Impulse);
                print(rb.velocity.x);
                hit = true;
                timer = 0.2f;
            }
            */
            
        }
    }
    
}
