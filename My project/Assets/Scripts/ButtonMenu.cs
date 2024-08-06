using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour
{
    public void TrocaCena(string cena)
    {
        // Toca o som pro botao
        PlayButtonClickSound();
        
        // Carrega a cena desejada
        SceneManager.LoadScene(cena);
    }

    // Carrega a cena anterior e reinicia a música
    public void LoadPreviousScene(string cena, string musica)
    {
        // Toca o som de clique do botao
        PlayButtonClickSound();

        AudioManager.instance.ReiniciaMusica(musica);
        SceneManager.LoadScene(cena);
    }

    private void PlayButtonClickSound()
    {
        AudioManager.instance.PlayButtonClickSFX();
    }
}
