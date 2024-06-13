using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour
{
    public void TrocaCena(string cena)
    {
        SceneManager.LoadScene(cena);
    }
}
