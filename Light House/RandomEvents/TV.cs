using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour , Interactable
{
    private AudioSource tvAudio;
    [SerializeField] private RandomEvents _RandomEvent;

    private void OnEnable()
    {
        if (tvAudio == null)
        {
            tvAudio = GetComponent<AudioSource>();
        }
        tvAudio.Play();
    }
    public void Interact()
    {
        _RandomEvent.SetTVBackOff();
    }

    public void Interacted()
    {
        _RandomEvent.SetTVBackOff();
    }
}
