using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseCodeLamp : MonoBehaviour
{
    [SerializeField] private MorseCodePuzzle _mCode;

    private void Update()
    {
        if (gameObject.layer == LayerMask.NameToLayer("PickedUp"))
        {
            _mCode._LightBulbPickedUp = true;
        }
        else
        {
            _mCode._LightBulbPickedUp = false;
        }
    }
}
