using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;

    void Start()
    {
        if (AudioManager.instance == null)
        {
            AudioManager.instance = FindObjectOfType<AudioManager>();

            if (AudioManager.instance == null)
            {
                Debug.LogError("AudioManager não foi encontrado na cena.");
                return;
            }
        }

        // Definir os sliders para os volumes atuais
        if (AudioManager.instance.MusicSource != null)
        {
            musicSlider.value = AudioManager.instance.MusicSource.volume;
        }
        else
        {
            Debug.LogWarning("MusicSource não está atribuído no AudioManager.");
        }

        if (AudioManager.instance.SfxSource != null)
        {
            sfxSlider.value = AudioManager.instance.SfxSource.volume;
        }
        else
        {
            Debug.LogWarning("SfxSource não está atribuído no AudioManager.");
        }

        // Adicionar listeners aos sliders
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.instance.SetMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        AudioManager.instance.SetSFXVolume(volume);
    }
}
