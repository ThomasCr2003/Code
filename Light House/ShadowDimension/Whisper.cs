using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Whisper : MonoBehaviour
{
    private AudioSource _whispersAudio;
    private bool _startAddingVolume;
    private float _volumeAdding = 0.05f;

    private void Update()
    {
        if (_startAddingVolume)
        {
            _whispersAudio.volume += Time.deltaTime * _volumeAdding;
        }
    }


    private void OnEnable()
    {
        if (_whispersAudio == null)
        {
            _whispersAudio = GetComponent<AudioSource>();
        }
        StartCoroutine(AddVolume());
    }


    IEnumerator AddVolume()
    {
        _whispersAudio.volume = 0.1f;
        _startAddingVolume = true;
        yield return new WaitForSeconds(20);
        _startAddingVolume = false;
    }
}
