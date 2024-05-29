using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("DoorOpen", true);
            Invoke("CloseDoor", 5);                 //Closes the door after 5 seconds after being opened.
        }
    }
    private void CloseDoor()
    {
        animator.SetBool("DoorOpen", false);
    }
}