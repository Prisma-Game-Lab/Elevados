using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGameController : MonoBehaviour
{
    public void OnNaoSairButtonPressed()
    {
        // Verifica se a cena anterior era "game"
        string previousScene = SceneHistoryManager.instance.GetPreviousScene();

        if (previousScene == "game")
        {
            // Volta para a cena "game"
            SceneManager.LoadScene("game");
        }
        else
        {
            // Se não for a cena "game", vai para o menu principal
            SceneManager.LoadScene("MenuPrincipal");
        }
    }

    public void OnSairButtonPressed()
    {
        // Sair do jogo
        Application.Quit();
    }
}
