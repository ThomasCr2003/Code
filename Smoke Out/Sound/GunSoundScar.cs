using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSoundScar : MonoBehaviour
{
    [SerializeField] private AudioSource _Audio;

    public void ScarGunSound()
    {
        _Audio.Play();
    }

}
