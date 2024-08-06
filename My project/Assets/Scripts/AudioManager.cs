using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    // Efeito sonoro que será tocado ao pressionar botões
    public AudioClip buttonClickSFX;

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

    public void ReiniciaMusica(string nomeMusica)
    {
        // Verifica se a música atual é a mesma que está pedindo para tocar
        if (musicSource.clip != null && musicSource.clip.name == nomeMusica)
        {
            // Se já estiver tocando a música correta, não faça nada
            if (musicSource.isPlaying)
            {
                return;
            }
        }

        // Carregar a nova música
        AudioClip novaMusica = Resources.Load<AudioClip>(nomeMusica);
        if (novaMusica != null)
        {
            // Atribuir a nova música e tocar
            musicSource.clip = novaMusica;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Música '{nomeMusica}' não encontrada nos recursos.");
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
