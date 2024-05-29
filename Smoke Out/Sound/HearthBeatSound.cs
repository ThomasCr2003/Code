using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthBeatSound : MonoBehaviour
{
    [SerializeField] private AudioSource _Audio;

    public void PlayHearthBeatSound()
    {
        _Audio.Play();
    }

    public void StopPlayHeartBeatSound()
    {
        _Audio.Stop();
    }
}
