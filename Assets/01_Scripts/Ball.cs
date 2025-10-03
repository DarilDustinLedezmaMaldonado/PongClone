using UnityEngine;

public class Ball : MonoBehaviour
{
    public float initialSpeed = 8f;
    private Rigidbody2D rb;
    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("Start() llamado - Lanzando pelota");
        LaunchBall();
    }

    public void LaunchBall()
    {
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f)).normalized;
        rb.velocity = direction * initialSpeed;
        Debug.Log("Pelota lanzada con velocidad: " + rb.velocity);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            float hitFactor = (transform.position.y - collision.transform.position.y) / collision.collider.bounds.size.y;
            direction = new Vector2(-direction.x, hitFactor).normalized;
            rb.velocity = direction * initialSpeed;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            direction = new Vector2(direction.x, -direction.y);
            rb.velocity = direction * initialSpeed;
        }
    }
}
