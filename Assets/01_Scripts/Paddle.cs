using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Paddle : MonoBehaviour
{
    public float speed = 12f;
    public bool isPlayer1 = true; // W/S (true) o ↑/↓ (false)

    Rigidbody2D rb;
    float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isPlayer1)
        {
            if (Input.GetKey(KeyCode.W)) moveInput = 1f;
            else if (Input.GetKey(KeyCode.S)) moveInput = -1f;
            else moveInput = 0f;
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow)) moveInput = 1f;
            else if (Input.GetKey(KeyCode.DownArrow)) moveInput = -1f;
            else moveInput = 0f;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(0f, moveInput * speed);
    }
}
