using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDeath : MonoBehaviour
{
    public bool hit = false;
    public float timer = 0;
    public PhysicsMaterial2D phyMat;
    private Rigidbody2D _rb;
    [SerializeField] private int knockBackForce;

    public PlayerHealth playerHealth;
    
    private int _layermask;


    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        _rb = GetComponent<Rigidbody2D>();
        timer = 0.5f;
        _layermask = LayerMask.GetMask("Enemy");
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
        if (collision.gameObject.CompareTag("Laser"))
        {
            StartCoroutine(Bounceback(collision.transform.position, knockBackForce));
        }
        if (collision.gameObject.CompareTag("Spikes"))
        {
            StartCoroutine(Bounceback(collision.transform.position, 3));
        }
        
    }
    public IEnumerator Bounceback(Vector3 laser, int knockback)
    {
        GameManager.instance.DecreaseHealth(1);
        if (playerHealth != null)
        {
            if (playerHealth.health != 0)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
                Vector3 pos = transform.position - laser;
                _rb.velocity = new Vector2(pos.x * knockback, 7);
                if (_rb.velocity.x >= 7)
                {
                    if (transform.position.x < laser.x)
                    {
                        _rb.velocity = new Vector2(-7, 7);
                    }
                    else
                    {
                        _rb.velocity = new Vector2(7, 7);
                    }
                }

                yield return new WaitForSeconds(0.2f);
                GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
            }
        }
    }
    
}
