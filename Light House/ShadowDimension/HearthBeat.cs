using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthBeat : MonoBehaviour
{
    [SerializeField] private AudioSource _AudioSource;
    private Sanity sanity;


    private void Start()
    {
        sanity = GetComponent<Sanity>();
    }


}
