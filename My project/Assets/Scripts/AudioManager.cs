using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Background Musics")]
    public List<AudioClip> backgroundMusics; // Lista de músicas de fundo

    [Header("Button SFX")]
    public AudioClip buttonClickSFX;

    private int currentMusicIndex;

    void Awake()
    {
        // Garantir que só exista um AudioManager (Singleton)
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persistir apenas o AudioManager entre as cenas
        }
        else
        {
            Destroy(gameObject); // Destruir a nova instância se já existir uma instância ativa
            return;
        }

        // Carregar os volumes salvos (ou definir padrão se não houver nada salvo)
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        // Verifica a cena atual e ajusta a música
        CheckAndPlaySceneMusic();
    }

    void OnEnable()
    {
        // Adiciona um listener para mudanças de cena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Remove o listener para evitar erros ao destruir objetos
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Checa e toca a música correta sempre que uma nova cena é carregada
        CheckAndPlaySceneMusic();
    }

    private void CheckAndPlaySceneMusic()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        // Determina o índice da música baseado na cena
        int musicIndex = sceneName == "game" ? 1 : 0; // 0 para o menu, 1 para o jogo

        // Se a música é diferente da atual, reinicia a música com a nova
        if (musicIndex != currentMusicIndex)
        {
            ReiniciaMusica(musicIndex);
            currentMusicIndex = musicIndex;
        }
    }

    // Método para tocar o som de clique do botão
    public void PlayButtonClickSFX()
    {
        if (buttonClickSFX != null)
        {
            sfxSource.PlayOneShot(buttonClickSFX);
        }
        else
        {
            Debug.LogWarning("No button click SFX assigned in AudioManager.");
        }
    }

    public void ReiniciaMusica(int musicIndex)
    {
        if (backgroundMusics.Count > musicIndex && musicIndex >= 0)
        {
            // Para a música atual
            musicSource.Stop();

            // Toca a nova música
            musicSource.clip = backgroundMusics[musicIndex];
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music index out of range.");
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume); // Salvar o volume
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume); // Salvar o volume
    }
}
