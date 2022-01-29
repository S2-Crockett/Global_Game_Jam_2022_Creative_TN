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
    

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        timer = 0.5f;
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
        if (collision.gameObject.CompareTag("Laser") || collision.gameObject.CompareTag("Spikes"))
        {
            StartCoroutine(Bounceback(collision.transform));
        }
    }
    IEnumerator Bounceback(Transform laser)
    {
        GetComponent<PlayerMovement>().enabled = false;
        Vector3 pos = transform.position - laser.position ;
        _rb.velocity = new Vector2(pos.x * knockBackForce, 7);
        yield return new WaitForSeconds(0.5f);
        GetComponent<PlayerMovement>().enabled = true;
    }
    
}
