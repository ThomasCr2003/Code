using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class Sanity : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Teleport _Teleport;
    [SerializeField] private Volume v;
    [SerializeField] private GameObject[] _Whispers;
    private int RandomNumber;
    public bool ShadowDimension;
    private bool _IsPlayingEndingCutscene;

    [Header("Sanity Stuff")]
    [SerializeField] private float _Sanity = 100;
    [SerializeField] private float _SanityDrain = 0.0725f;
    [SerializeField] private float _SanityRestore;
    [SerializeField] private float _SanityMultiplier;
    private float maxSanity = 100;
    private float sanity0;
    private float sanityRestoreTime = 5;
    private float timer;

    [Header("Vignette Effect")]
    private Vignette vignette;
    [SerializeField] private float _VignetteAdding;
    [SerializeField] private float _VignetteSmoothnessAdd;
    [SerializeField] private float _VignetteSmoothnessMin;
    [SerializeField] private float _VignetteSmoothnessMax;
    [SerializeField] private float _VignetteSmoothnessMultiplier;
    private bool vignetteeffectreset;

    [Header("HearthBeat")]
    [SerializeField] private AudioSource HearthBeatAudio;
    private bool hearthBeatPlaying;
    
    private void Start()
    {
        Vignette tmp;
        if (v.profile.TryGet<Vignette>(out tmp))
        {
            vignette = tmp;
        }
    }

    private void Update()
    {
        if (GetCurrentDimension())
        {
            DepleteSanity();
        }
        else
        {
            RestoreSanity();
        }
    }

    private void DepleteSanity()
    {
        if (_Sanity > sanity0 && !_IsPlayingEndingCutscene)
        {
            _Sanity -= _SanityDrain + Time.deltaTime;
            vignette.intensity.value += _VignetteAdding + Time.deltaTime / _SanityMultiplier; 
            
        }

        if (_Sanity <= 80)
        {
            StartHeartBeat();
        }
    }

    private void RestoreSanity()
    {
        if (_Sanity <= maxSanity) //If Sanity isnt Restored
        {
            if (timer <= sanityRestoreTime) //StartTimer To restore Sanity
            {
                timer += Time.deltaTime;
            }
            else
            {
                _Sanity +=_SanityRestore + Time.deltaTime;
                vignette.intensity.value -= _VignetteAdding + Time.deltaTime / (_SanityMultiplier - 4);
                if (vignette.smoothness.value <= _VignetteSmoothnessMax && !vignetteeffectreset)
                {
                    vignette.smoothness.value += _VignetteSmoothnessAdd + Time.deltaTime / _VignetteSmoothnessMultiplier;
                    if (vignette.smoothness.value >= _VignetteSmoothnessMax)
                    {
                        vignetteeffectreset = true;
                        vignette.smoothness.value = _VignetteSmoothnessMax;
                    }
                }
                else if (vignette.smoothness.value <= _VignetteSmoothnessMax && vignetteeffectreset)
                {
                    vignette.smoothness.value -= _VignetteSmoothnessAdd + Time.deltaTime /_VignetteSmoothnessMultiplier;
                    if (vignette.smoothness.value <= _VignetteSmoothnessMin)
                    {
                        vignetteeffectreset = false;
                        vignette.smoothness.value = _VignetteSmoothnessMin;
                    }
                }
            }
        }
        if (_Sanity >= maxSanity)
        {
            _Sanity = maxSanity;
            vignette.smoothness.value = _VignetteSmoothnessMin;
            vignette.intensity.value -= _VignetteSmoothnessAdd + Time.deltaTime / _VignetteSmoothnessMultiplier;
            ResetTimer();
            return;
        }
        if (_Sanity >= 80)
        {
            StopHeartbeat();
        }
    }

    private void StartHeartBeat()
    {
        if (!hearthBeatPlaying)
        {
            HearthBeatAudio.Play();
            hearthBeatPlaying = true;
        }
    }

    private void StopHeartbeat()
    {
        if (hearthBeatPlaying)
        {
            HearthBeatAudio.Stop();
            hearthBeatPlaying = false;
        }
    }

    public void ResetTimer()
    {
        timer = 0;
    }

    public float GetCurrentSanity()
    {
        return _Sanity;
    }

    /// <summary>
    /// If True Player is in ShadowDimension. If False Player is in normal world.
    /// </summary>
    /// <returns></returns>
    public bool GetCurrentDimension()
    {
        return ShadowDimension;
    }

    public void RandomizeWhisper()
    {
        RandomNumber = Random.Range(0, _Whispers.Length);
        if (RandomNumber == 0)
        {
            _Whispers[0].SetActive(true);
        }
        else
        {
            _Whispers[1].SetActive(true);
        }
    }

    public void TurnOffWhisper()
    {
        if (RandomNumber == 0)
        {
            _Whispers[0].SetActive(false);
        }
        else
        {
            _Whispers[1].SetActive(false);
        }
    }

    public void RemoveVignette()
    {
        vignette.smoothness.value = 0;
        vignette.intensity.value = 0;
        _IsPlayingEndingCutscene = true;
    }
}
