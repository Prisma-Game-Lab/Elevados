using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class codigo : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenuInitial;
    [SerializeField] private GameObject painelOptions;

    public void Play(){ SceneManager.LoadScene(nomeDoLevelDeJogo); }

    public void OpenOptions() 
    {
        painelMenuInitial.SetActive(false);
        painelOptions.SetActive(true);
    }

    public void CloseOptions() 
    {
        painelOptions.SetActive(false);
        painelMenuInitial.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
}
