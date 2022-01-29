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
        if (col.CompareTag("Enemy") && _rigidbody2D.velocity.y < -0.1)
        {
            Destroy(col.gameObject);
        }
    }
}
