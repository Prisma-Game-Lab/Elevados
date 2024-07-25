using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Para carregar cenas de vitória/derrota
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton

    private int score = 0;
    public int targetScore = 15; // Pontuação necessária para ganhar
    public event Action<int> onScoreChanged; // Evento de mudança de pontuação

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Pontuação atual: " + score);
        onScoreChanged?.Invoke(score); // Notifica os inscritos sobre a mudança de pontuação
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        if (score >= targetScore)
        {
            // Carrega a cena de vitória
            SceneManager.LoadScene("WinScene");
        }
    }

    public void GameOver()
    {
        // Carrega a cena de derrota
        SceneManager.LoadScene("GameOverScene");
    }

    public int GetScore()
    {
        return score;
    }
}
