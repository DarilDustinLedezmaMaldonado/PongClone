using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Button restartButton; // Nuevo bot�n de reinicio

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
        // Patr�n Singleton
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
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

        // Configurar el bot�n de reinicio
        
    }

    // M�todos p�blicos para cambiar nombres desde otros scripts
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
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        int winnerScore = Mathf.Max(player1Score, player2Score);

        if (winnerScore > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", winnerScore);
            PlayerPrefs.Save();
        }

    }       

    // M�todo para reiniciar el juego (llamado por el bot�n)
    public void RestartGame()
    {
        Debug.Log("Juego reiniciado!");

        // Recargar la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Reasignar referencias despu�s de que la escena cargue
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Buscar objetos de UI en la nueva escena
        gameOverPanel = GameObject.Find("GameOverPanel"); // Nombre exacto
        gameOverText = GameObject.Find("GameOverText").GetComponent<TMP_Text>();
        restartButton = GameObject.Find("RestartButton").GetComponent<Button>();

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);

        ResetScores();

        // Desuscribirse para que no se ejecute m�s veces
        SceneManager.sceneLoaded -= OnSceneLoaded;
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

    // M�todos para obtener informaci�n (para otros scripts)
    public int GetPlayer1Score() => player1Score;
    public int GetPlayer2Score() => player2Score;
    public string GetPlayer1Name() => player1Name;
    public string GetPlayer2Name() => player2Name;
    public bool IsGameActive() => gameActive;
}