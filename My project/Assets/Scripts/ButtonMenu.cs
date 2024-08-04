using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour
{
    public void TrocaCena(string cena)
    {
        SceneManager.LoadScene(cena);
    }

    // Carrega a cena anterior e reinicia a música
    public void LoadPreviousScene(string cena, string musica)
    {
        AudioManager.instance.ReiniciaMusica(musica);
        SceneManager.LoadScene(cena);
    }
}
