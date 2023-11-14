using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsControlScript : MonoBehaviour
{
    public Slider volumeSlider;
    public TextMeshProUGUI volumeText;
    public Slider brightnessSlider;
    public TextMeshProUGUI brightnessText;

    public void update()
    {
        UpdateVolume(volumeSlider.value);
        UpdateBrightness(brightnessSlider.value);
    }

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        brightnessSlider.value = PlayerPrefs.GetFloat("Brightness", 1f);

        volumeSlider.onValueChanged.AddListener(UpdateVolume);
        brightnessSlider.onValueChanged.AddListener(UpdateBrightness);

        UpdateVolume(volumeSlider.value);
        UpdateBrightness(brightnessSlider.value);
    }

    public void UpdateVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeText.text = "" + (int)(volume * 100) + "%";
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void UpdateBrightness(float brightness)
    {
        RenderSettings.ambientLight = new Color(brightness, brightness, brightness, 1f);
        brightnessText.text = "" + (int)(brightness * 100) + "%";
        PlayerPrefs.SetFloat("Brightness", brightness);
    }
}
