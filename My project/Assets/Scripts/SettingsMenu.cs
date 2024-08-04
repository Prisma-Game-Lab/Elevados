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
        // Definir os sliders para os volumes atuais
        musicSlider.value = AudioManager.instance.musicSource.volume;
        sfxSlider.value = AudioManager.instance.SFXSource.volume;

        // Adicionar listeners aos sliders para chamar os métodos de ajuste de volume
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
