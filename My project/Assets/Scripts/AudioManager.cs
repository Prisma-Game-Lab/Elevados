using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    public AudioClip[] musicas, efeitosSonoros; // Arrays para armazenar as músicas e os efeitos sonoros

    [SerializeField]
    public AudioSource MusicSource, SfxSource; // AudioSources para tocar música e efeitos sonoros

    // void Awake()
    // {
    //     // Singleton pattern
    //     if (instance == null)
    //     {
    //         instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    // Toca uma música pelo nome
    public void TocaMusica(string nomeMusica)
    {
        // Verifica se a música atual já é a que está tocando
        if (MusicSource.clip != null && MusicSource.clip.name == nomeMusica && MusicSource.isPlaying)
        {
            ReiniciaMusica(nomeMusica);
        }

        AudioClip musica = Array.Find(musicas, x => x.name == nomeMusica);

        if (musica == null)
        {
            Debug.Log("Música não encontrada");
            return;
        }

        MusicSource.clip = musica;
        MusicSource.Play();
    }

    // Toca um efeito sonoro pelo nome
    public void TocaEfeitoSonoro(string nomeEfeito)
    {
        AudioClip efeito = Array.Find(efeitosSonoros, x => x.name == nomeEfeito);

        if (efeito == null)
        {
            Debug.Log("Efeito sonoro não encontrado");
            return;
        }

        SfxSource.clip = efeito;
        SfxSource.Play();
    }

    public void ReiniciaMusica(string nomeMusica)
    {
        // Para a música atual
        MusicSource.Stop();

        // Inicia a música novamente
        TocaMusica(nomeMusica);
    }

    public void SetMusicVolume(float volume)
    {
        MusicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        SfxSource.volume = volume;
    }
}
