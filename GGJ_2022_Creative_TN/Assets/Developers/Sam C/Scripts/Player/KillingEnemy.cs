using UnityEngine;

public class KillingEnemy : MonoBehaviour
{

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            if (_rigidbody2D.velocity.y < -0.1)
            {
                Destroy(col.gameObject);
                GameManager.instance.UpdateScore(100);
            }
            else 
            {
                if (CompareTag("Player"))
                {
                    StartCoroutine(GetComponent<ParticleDeath>().Bounceback(col.transform.position));
                }
                else if (CompareTag("Player Two"))
                {
                    Vector3 offset = GetComponent<ShadowPlayer>().offset;
                    ParticleDeath knockBack = GameObject.FindGameObjectWithTag("Player").GetComponent<ParticleDeath>();
                    StartCoroutine(knockBack.Bounceback(col.transform.position + offset));
                }
            }
        }
    }
}
