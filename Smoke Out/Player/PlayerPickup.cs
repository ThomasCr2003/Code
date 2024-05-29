using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private CollectSound _CollectSound;
    private void OnTriggerEnter(Collider other)
    {
        IInteratable _interactable = other.gameObject.GetComponent<IInteratable>();
        if (_interactable != null)
        {
            _CollectSound.PickupSound();
            _interactable.Interact();
        }
    }
}
