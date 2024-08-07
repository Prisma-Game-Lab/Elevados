using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    void Update()
    {
        // Verifica se a tecla "Esc" foi pressionada
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Armazena o nome da cena atual antes de mudar para a cena "Quit"
            SceneHistoryManager.instance.SetPreviousScene(SceneManager.GetActiveScene().name);

            // Carrega a cena "Quit"
            SceneManager.LoadScene("Quit");
        }
    }
}
