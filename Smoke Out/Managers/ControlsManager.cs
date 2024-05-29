using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ControlsManager : MonoBehaviour
{
    public static ControlsManager Instance { get; private set; }
    public AudioMixer audioMixer;
    public bool isHeadBobbing = true;
    public float SensX;
    public float SensY;
    public float Volume;
    public Slider VolumeSliderValue;
    public Slider SensSliderValueX;
    public Slider SensSliderValueY;
    public Toggle HeadBobbingToggle;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

        GetSavedSettings();
        SetValue();
    }


    /// <summary>
    /// Gets the current Settings.
    /// </summary>
    public void GetSavedSettings()
    {
        isHeadBobbing = PlayerPrefs.GetInt(PlayerPrefsDefinitions.HeadBobbing) == 1;
        SensX = PlayerPrefs.GetFloat(PlayerPrefsDefinitions.SensX);
        SensY = PlayerPrefs.GetFloat(PlayerPrefsDefinitions.SensY);
        Volume = PlayerPrefs.GetFloat(PlayerPrefsDefinitions.Volume);
    }

    public void SetValue()
    {
        VolumeSliderValue.value = Volume;
        SensSliderValueX.value = SensX;
        SensSliderValueY.value = SensY;
        HeadBobbingToggle.isOn = isHeadBobbing;
    }

    public void VolumeSlider(float _volume)
    {
        audioMixer.SetFloat(PlayerPrefsDefinitions.Volume, _volume);
        PlayerPrefs.SetFloat(PlayerPrefsDefinitions.Volume, _volume);
    }

    public void SensetivitySliderX(float _sens)
    {
        PlayerPrefs.SetFloat(PlayerPrefsDefinitions.SensX, _sens);
    }

    public void SensetivitySliderY(float _sens)
    {
        PlayerPrefs.SetFloat(PlayerPrefsDefinitions.SensY, _sens);
    }


    public void HeadBobbing(bool isHeadBobbing)
    {
        PlayerPrefs.SetInt(PlayerPrefsDefinitions.HeadBobbing, isHeadBobbing ? 1:0);
    }
}
