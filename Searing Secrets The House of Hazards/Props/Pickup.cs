using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool HasKey;

    [SerializeField] private GameObject _CurrentObject;
    [SerializeField] private GameObject _CurrentObjectPlaceHolder;
    [SerializeField] private bool _IsPickedUp;

    [SerializeField] private float _RotationReset;
    [SerializeField] private int _RotationInHand;

    public Animation DoorAnimation;
    public void Interactable(GameObject pos)
    {
        if (!_IsPickedUp)
        {
            _CurrentObject.transform.parent = pos.transform;
            transform.position = pos.transform.position;
            _CurrentObject.transform.localRotation = Quaternion.Euler(0, _RotationInHand, 0);
            _IsPickedUp = true;
        }
    }

    public Animation GetDoorAnimation()
    {
        return DoorAnimation;
    }
}
