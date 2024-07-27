using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        // Inicializa a pontuação com o valor atual
        if (ScoreManager.instance != null)
        {
            UpdateScore(ScoreManager.instance.GetScore());
            // Inscreve-se no evento de mudança de pontuação
            ScoreManager.instance.onScoreChanged += UpdateScore;
        }
        else
        {
            Debug.LogError("ScoreManager.instance é nulo no Start do ScoreUI.");
        }
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
        if (scoreText != null)
        {
            scoreText.text += newScore.ToString();
        }
        else
        {
            Debug.LogError("scoreText é nulo em UpdateScore.");
        }
    }
}
