using System;
using UnityEngine;

public class ShadowPlayer : MonoBehaviour
{
    private GameObject _player;
    
    public Vector3 offset;
    
    
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    

    private void Update()
    {
        Vector3 posOffset = new Vector3(0.6f, 0, 0);
        if (Physics2D.Raycast(transform.position - posOffset, Vector2.down, Mathf.Infinity) ||
            Physics2D.Raycast(transform.position + posOffset, Vector2.down, Mathf.Infinity) )
        {
            Vector3 pos = new Vector3(_player.transform.position.x * 1, _player.transform.position.y,
                _player.transform.position.z);
            transform.position = pos + offset;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Laser") || collision.gameObject.CompareTag("Spikes"))
        {
            StartCoroutine(_player.GetComponent<ParticleDeath>().Bounceback(collision.transform.position + offset));
        }
    }
}
