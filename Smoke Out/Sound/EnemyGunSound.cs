using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunSound : MonoBehaviour
{
    [SerializeField] private AudioSource _Audio;

    public void EnemyGunShootSound()
    {
        _Audio.Play();
    }
}
