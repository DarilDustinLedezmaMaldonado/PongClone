using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Text highScoreText;

    void Start()
    {
        LoadHighScore();
    }

    void LoadHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // Nombre exacto de la escena del juego
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Juego cerrado en editor."); // Para pruebas en Unity Editor
    }
}
