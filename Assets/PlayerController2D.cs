using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode jumpKey;
    public KeyCode shootKey;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 100f;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        firePoint = transform.Find("FirePoint");
    }

    void Update()
    {
        // Movement
        float move = 0f;

        if (Input.GetKey(leftKey))
        {
            move = -1f;
            transform.localScale = new Vector3(-.25f, .25f, .25f); // face left
            firePoint.localPosition = new Vector3(-4.24f, 2.64f, 0f);
        }
        else if (Input.GetKey(rightKey))
        {
            move = 1f;
            transform.localScale = new Vector3(.25f, .25f, .25f); // face right
            firePoint.localPosition = new Vector3(-4.24f, 2.64f, 0f);
        }

        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);


        // Jump
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Shoot
        if (Input.GetKeyDown(shootKey))
        {
            Shoot();
        }

        // Death if falling
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        bulletRb.linearVelocity = direction * bulletSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
            isGrounded = true;
    }

    void OnCollisionExit2D()
    {
        isGrounded = false;
    }
}
