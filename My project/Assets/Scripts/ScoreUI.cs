using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        // Inicializa a pontuação com o valor atual
        UpdateScore(ScoreManager.instance.GetScore());
        
        // Inscreve-se no evento de mudança de pontuação
        ScoreManager.instance.onScoreChanged += UpdateScore;
    }

    void OnDestroy()
    {
        // Remove a inscrição no evento quando este objeto for destruído
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.onScoreChanged -= UpdateScore;
        }
    }

    void UpdateScore(int newScore)
    {
        scoreText.text = "Gorjeta: " + newScore.ToString();
    }
}
