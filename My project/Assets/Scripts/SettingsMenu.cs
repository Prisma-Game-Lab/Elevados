using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        // Atribuir as funções de callback para os sliders
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        // Definir os sliders para refletir os volumes salvos
        if (AudioManager.instance != null)
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        }
        else
        {
            Debug.LogError("AudioManager instance is null! Ensure AudioManager is properly initialized.");
        }
    }

    private void SetMusicVolume(float volume)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetMusicVolume(volume);
        }
        else
        {
            Debug.LogError("AudioManager instance is null! Ensure AudioManager is properly initialized.");
        }
    }

    private void SetSFXVolume(float volume)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetSFXVolume(volume);
        }
        else
        {
            Debug.LogError("AudioManager instance is null! Ensure AudioManager is properly initialized.");
        }
    }
}
