using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Cronometro : MonoBehaviour
{
    public TextMeshProUGUI cronometroText;
    public int tempoInicialEmSegundos = 180; // 3 minutos em segundos
    private float tempoRestante;
    private bool cronometroAtivo = false;
    private bool elevadorSaiuDoAndarInicial = false;

    void Start()
    {
        tempoRestante = tempoInicialEmSegundos;
        AtualizarCronometroText();
    }

    void Update()
    {
        if (cronometroAtivo)
        {
            tempoRestante -= Time.deltaTime;
            AtualizarCronometroText();

            if (tempoRestante <= 0)
            {
                cronometroAtivo = false;
                tempoRestante = 0;
                AtualizarCronometroText();
                GameOver();
            }
        }
    }

    public void IniciarCronometro()
    {
        cronometroAtivo = true;
    }

    public void PararCronometro()
    {
        cronometroAtivo = false;
    }

    private void AtualizarCronometroText()
    {
        int minutos = Mathf.FloorToInt(tempoRestante / 60);
        int segundos = Mathf.FloorToInt(tempoRestante % 60);
        cronometroText.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    private void GameOver()
    {
        // Lógica de fim de jogo, como exibir a pontuação final
        Debug.Log("Game Over!");
        SceneManager.LoadScene("GameOverScene"); // Assumindo que você tem uma cena de Game Over
    }

    public void ElevadorSaiuDoAndarInicial()
    {
        if (!elevadorSaiuDoAndarInicial)
        {
            elevadorSaiuDoAndarInicial = true;
            IniciarCronometro();
        }
    }
}
