using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI References")]
    public TMP_Text player1NameText;
    public TMP_Text player1ScoreText;
    public TMP_Text player2NameText;
    public TMP_Text player2ScoreText;
    public TMP_Text gameOverText;
    public GameObject gameOverPanel;
    public Button restartButton; // Nuevo botón de reinicio

    [Header("Player Settings")]
    public string player1Name = "Player 1";
    public string player2Name = "Player 2";

    [Header("Score Settings")]
    public int winScore = 5; // Puntos para ganar

    private int player1Score = 0;
    private int player2Score = 0;
    private bool gameActive = true;

    void Awake()
    {
        // Patrón Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdatePlayerNames();
        UpdateScoreDisplay();

        // Ocultar panel de game over al inicio
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Configurar el botón de reinicio
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    // Métodos públicos para cambiar nombres desde otros scripts
    public void SetPlayer1Name(string name)
    {
        player1Name = name;
        UpdatePlayerNames();
    }

    public void SetPlayer2Name(string name)
    {
        player2Name = name;
        UpdatePlayerNames();
    }

    void UpdatePlayerNames()
    {
        if (player1NameText != null)
            player1NameText.text = player1Name;

        if (player2NameText != null)
            player2NameText.text = player2Name;
    }

    public void AddScore(bool isPlayer1)
    {
        if (!gameActive) return;

        if (isPlayer1)
        {
            player1Score++;
            Debug.Log(player1Name + " scored! Total: " + player1Score);
        }
        else
        {
            player2Score++;
            Debug.Log(player2Name + " scored! Total: " + player2Score);
        }

        UpdateScoreDisplay();
        CheckWinCondition();
    }

    void UpdateScoreDisplay()
    {
        if (player1ScoreText != null)
            player1ScoreText.text = player1Score.ToString();

        if (player2ScoreText != null)
            player2ScoreText.text = player2Score.ToString();
    }

    void CheckWinCondition()
    {
        if (player1Score >= winScore)
        {
            EndGame(player1Name);
        }
        else if (player2Score >= winScore)
        {
            EndGame(player2Name);
        }
    }

    void EndGame(string winnerName)
    {
        gameActive = false;
        Debug.Log("Game Over! Winner: " + winnerName);

        // Mostrar pantalla de game over si existe
        if (gameOverPanel != null && gameOverText != null)
        {
            gameOverText.text = winnerName + " GANO!!!";
            gameOverPanel.SetActive(true);
        }
    }

    // Método para reiniciar el juego (llamado por el botón)
    public void RestartGame()
    {
        ResetScores();
        Debug.Log("Juego reiniciado!");
    }

    public void ResetScores()
    {
        player1Score = 0;
        player2Score = 0;
        gameActive = true;
        UpdateScoreDisplay();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    // Métodos para obtener información (para otros scripts)
    public int GetPlayer1Score() => player1Score;
    public int GetPlayer2Score() => player2Score;
    public string GetPlayer1Name() => player1Name;
    public string GetPlayer2Name() => player2Name;
    public bool IsGameActive() => gameActive;
}