using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtiene el Rigidbody2D del objeto
    }

    void Update()
    {
        // Detectar entrada de WASD
        movement.x = Input.GetAxisRaw("Horizontal"); // A (-1) / D (+1)
        movement.y = Input.GetAxisRaw("Vertical");   // S (-1) / W (+1)
    }

    void FixedUpdate()
    {
        // Aplicar movimiento
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
