using System.Collections;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [Tooltip("Si true: esta zona es el gol del jugador 1 (es decir, cuando entra la pelota se suma al jugador 2).")]
    public bool isPlayer1Goal = false;

    [Tooltip("Punto donde reaparecerá la pelota (normalmente el centro). Si está vacío, se usará Vector3.zero.")]
    public Transform ballSpawnPoint;

    [Tooltip("Tiempo en segundos antes de lanzar la pelota nuevamente tras un gol.")]
    public float respawnDelay = 0.8f;

    [Tooltip("Sonido que suena al marcar un gol (opcional).")]
    public AudioClip goalSound;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && goalSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Ball")) return;

        // Sumar puntaje (Nota: ScoreManager debe tener el método AddScore(bool))
        ScoreManager.Instance.AddScore(!isPlayer1Goal);

        // Sonido
        if (goalSound != null && audioSource != null) audioSource.PlayOneShot(goalSound);

        // Reset / respawn de la pelota
        StartCoroutine(ResetBallAfterGoal(collision.gameObject));
    }

    IEnumerator ResetBallAfterGoal(GameObject ballGO)
    {
        Rigidbody2D ballRb = ballGO.GetComponent<Rigidbody2D>();
        Ball ballScript = ballGO.GetComponent<Ball>();

        // Desactivar movimiento inmediato
        if (ballRb != null) ballRb.velocity = Vector2.zero;

        // Reposicionar la pelota al spawnPoint (o centro si no hay spawn)
        Vector3 spawn = ballSpawnPoint ? ballSpawnPoint.position : Vector3.zero;
        ballGO.transform.position = spawn;

        // Opcional: animación de UI o pausa breve
        yield return new WaitForSeconds(respawnDelay);

        // Lanza la pelota con el método público del script Ball
        if (ballScript != null)
        {
            ballScript.LaunchBall();
        }
        else
        {
            // Si no existe Ball script, intentar reactivar la física directamente
            if (ballRb != null)
            {
                // lanzamiento de seguridad: dirección aleatoria hacia el rival
                Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f)).normalized;
                ballRb.velocity = dir * 6f;
            }
        }
    }
}
