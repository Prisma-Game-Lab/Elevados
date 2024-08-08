using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton

    public int score;
    public int targetScore; // Pontuação necessária para ganhar
    public event Action<int> onScoreChanged; // Evento de mudança de pontuação
    [SerializeField] private TextMeshProUGUI scoreText; // Referência ao componente de texto

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
    void Start()
    {
        // Atualiza o texto com a pontuação inicial
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Pontuação atual: " + score);
        onScoreChanged?.Invoke(score); // Notifica os inscritos sobre a mudança de pontuação
        UpdateScoreText(); // Atualiza o texto com a nova pontuação
        CheckWinCondition();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    private void CheckWinCondition()
    {
        if (score >= targetScore)
        {
            // Chama o método de vitória no MonsterManager
            MonsterManager monsterManager = FindObjectOfType<MonsterManager>();
            if (monsterManager != null)
            {
                monsterManager.TriggerVictory();
            }
        }
    }
}
