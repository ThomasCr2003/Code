using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knocking : MonoBehaviour
{
    [SerializeField] private float _DestroyTimer;

    void Start()
    {
        Destroy(gameObject,_DestroyTimer);
    }

}
