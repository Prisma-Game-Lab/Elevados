using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class Cronometro : MonoBehaviour
{
    public TextMeshProUGUI cronometroText;
    public int tempoInicialEmSegundos; // minutos em segundos
    private float tempoRestante; 
    private bool cronometroAtivo = false;
    private bool elevadorSaiuDoAndarInicial = false;
    [SerializeField] private GameObject gameOverMessage; // Associe no Inspector

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
        // Exibe a mensagem de Game Over
        ShowGameOverMessage();

        // Aguarda alguns segundos e volta ao menu principal
        StartCoroutine(ReturnToMenuAfterDelay());
    }

    private void ShowGameOverMessage()
    {
        if (gameOverMessage != null)
        {
            gameOverMessage.SetActive(true);
        }
    }

    private IEnumerator ReturnToMenuAfterDelay()
    {
        yield return new WaitForSeconds(5); // Aguarda 5 segundos

        // Carrega a cena do menu principal
        SceneManager.LoadScene("Menu");
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
