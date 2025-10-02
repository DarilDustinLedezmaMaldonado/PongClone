using UnityEngine;

public class TestScoreSystem : MonoBehaviour
{
    void Update()
    {
        // Prueba con teclas - QUITAR después de probar
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ScoreManager.Instance.AddScore(true);
            Debug.Log("Punto para Player 1");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ScoreManager.Instance.AddScore(false);
            Debug.Log("Punto para Player 2");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ScoreManager.Instance.ResetScores();
            Debug.Log("Scores reseteados");
        }
    }
}