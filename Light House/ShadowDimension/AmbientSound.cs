using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    [SerializeField] private AudioSource Normal, Shadow;

    private void Start()
    {
        PlayNormalAmbient();
    }

    public void PlayNormalAmbient()
    {
        Shadow.Stop();
        Normal.Play();
    }

    public void PlayShadowAmbient()
    {
        Normal.Stop();
        Shadow.Play();
    }
}
