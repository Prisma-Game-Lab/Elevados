using UnityEngine;

public class ButtonQuit : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("saiu do jogo");
        Application.Quit();
    }
}
