using UnityEngine;

public class Paddle : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Velocidad de movimiento vertical del paddle")]
    public float speed = 10f;

    [Tooltip("Eje vertical mínimo y máximo para limitar el movimiento")]
    public float yMin = -4f;
    public float yMax = 4f;

    [Header("Controles")]
    public string inputAxis = "Vertical"; 
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        float move = Input.GetAxis(inputAxis);

 
        float newY = Mathf.Clamp(transform.position.y + move * speed * Time.deltaTime, yMin, yMax);


        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
