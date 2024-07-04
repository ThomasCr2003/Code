using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioMixer MainAudioMixer;
    public AudioMixer VFXAudioMixer;
    public float VFXVolume;
    public float MainVolume;
    public Slider MainVolumeSlider;
    public Slider VFXVolumeSlider;
    [SerializeField] private GameObject _BackGround;
    private bool _pauseGame;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

        GetSavedSettings();
        SetValue();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!_pauseGame)
            {
                _BackGround.SetActive(true);
                GetSavedSettings();
                SetValue();
                Time.timeScale = 0;
                _pauseGame = true;
            }
            else
            {
                _BackGround.SetActive(false);
                PlayerScream.Instance.SetCanScream(true);
                Time.timeScale = 1;
                _pauseGame = false;
            }
        }

        while (_pauseGame)
        {
            PlayerScream.Instance.SetCanScream(false);
            PlayerScream.Instance.SetScreamindicatorOff();
            break;
        }
    }

    /// <summary>
    /// Gets the current Settings.
    /// </summary>
    public void GetSavedSettings()
    {
        VFXVolume = PlayerPrefs.GetFloat(PlayerPrefsDefinitions.VFXVolume);
        MainVolume = PlayerPrefs.GetFloat(PlayerPrefsDefinitions.MainVolume);
    }

    public void SetValue()
    {
        MainVolumeSlider.value = MainVolume;
        VFXVolumeSlider.value = VFXVolume;
    }

    public void VolumeMainSlider(float _volume)
    {
        MainAudioMixer.SetFloat(PlayerPrefsDefinitions.MainVolume, _volume);
        PlayerPrefs.SetFloat(PlayerPrefsDefinitions.MainVolume, _volume);
    }

    public void VolumeVFXSlider(float _volume)
    {
        VFXAudioMixer.SetFloat(PlayerPrefsDefinitions.VFXVolume, _volume);
        PlayerPrefs.SetFloat(PlayerPrefsDefinitions.VFXVolume, _volume);
    }
}
