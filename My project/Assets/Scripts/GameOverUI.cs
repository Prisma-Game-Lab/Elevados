using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void RestartGame()
    {
        // Reinicie o jogo carregando a cena inicial
        SceneManager.LoadScene("NomeDaSuaCenaInicial");
    }

    public void QuitGame()
    {
        // Fecha o jogo (apenas funciona na build, não no editor)
        Application.Quit();
    }
}
