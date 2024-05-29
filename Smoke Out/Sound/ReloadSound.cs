using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadSound : MonoBehaviour
{
    [SerializeField] private AudioSource _Audio;

    public void PlayReloadSound()
    {
        _Audio.Play();
    }
}
