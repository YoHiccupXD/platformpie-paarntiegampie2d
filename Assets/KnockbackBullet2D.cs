using UnityEngine;

public class KnockbackBullet2D : MonoBehaviour
{
    public float knockbackForce = 12f;
    public float lifetime = 2f;

    void Start()
    {
    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 dir = (collision.transform.position - transform.position).normalized;
            rb.AddForce(dir * knockbackForce, ForceMode2D.Impulse);
        }

        Destroy(gameObject);
    }
}
