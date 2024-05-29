using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositMoneySound : MonoBehaviour
{
    [SerializeField] private AudioSource _Audio;

    public void PlayDepositMoneyAudio()
    {
        _Audio.Play();
    }
}
