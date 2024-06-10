using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] musicas,
        efeitosSonoros;

    [SerializeField]
    private AudioSource musicSource,
        SFXSource;

    public void TocaMusica(string nomeMusica)
    {
        AudioClip musica = Array.Find(musicas, x => x.name == nomeMusica);

        if (musica == null)
        {
            Debug.Log("Música não encontrada");
            return;
        }

        musicSource.clip = musica;
        musicSource.Play();
    }

    public void TocaEfeitoSonoro(string nomeEfeito)
    {
        AudioClip efeito = Array.Find(musicas, x => x.name == nomeEfeito);

        if (efeito == null)
        {
            Debug.Log("Música não encontrada");
            return;
        }

        SFXSource.clip = efeito;
        SFXSource.Play();
    }

    void Start()
    {
        if (gameObject != null)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
