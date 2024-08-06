using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    //Monitora o estado do elevador: 0 se tds os botoes estiverem em posicao neutra
    //1 se algum botao estiver apertado
    private int buttonsReleased;

    // Referência ao AudioManager
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        buttonsReleased = 0;

        // Inicializa a referência ao AudioManager
        audioManager = AudioManager.instance;

        if (audioManager == null)
        {
            Debug.LogError("AudioManager instance is null! Ensure AudioManager is properly initialized.");
        }
    }

    public int hold()
    {
        // Toca o som de clique do botão, se possível
        if (audioManager != null)
        {
            audioManager.PlayButtonClickSFX();
        }
        
        //Retorna 1 se for possivel apertar o botao
        if (buttonsReleased == 0)
        {
            buttonsReleased = 1;
            return 1;
        }

        //Retorno 0 caso contrario
        return 0;
    }

    public void release()
    {
        buttonsReleased = 0;
    }
}
