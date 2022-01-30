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
    

    
    private int _layermask;


    void Start()
    {
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
        if (collision.gameObject.CompareTag("Laser") || collision.gameObject.CompareTag("Spikes"))
        {
            StartCoroutine(Bounceback(collision.transform.position));
        }
    }
    public IEnumerator Bounceback(Vector3 laser)
    {
        GameManager.instance.DecreaseHealth(1);
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth.health != 0)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
            Vector3 pos = transform.position - laser;
            _rb.velocity = new Vector2(pos.x * knockBackForce, 7);
            yield return new WaitForSeconds(0.5f);
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        }
        else
        {
            playerHealth.IncreaseHealth(5);
            UIManager.instance.healthUI.InitHealth(5);
        }
    }
    
}
