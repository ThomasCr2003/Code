using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum KeycardDoors
{
    KeycardBlue,
    KeycardGreen,
    KeycardRed,
    Keycardpurple,
}
public class Keycard_Door : MonoBehaviour
{
    private Animator animator;
    public PlayerHealth player;
    [SerializeField] private KeycardDoors doorType;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>())
        {
            switch (doorType)
            {
                case KeycardDoors.KeycardBlue:                  //Checks if player has the correct keycard for the door.
                    if (player.keycardBlue) 
                    {
                        animator.SetBool("DoorOpen", true);
                        Invoke("CloseDoor", 5);
                    }
                    break;
                case KeycardDoors.KeycardGreen:
                    if (player.keycardGreen)
                    {
                        animator.SetBool("DoorOpen", true);
                        Invoke("CloseDoor", 5);
                    }
                    break;
                case KeycardDoors.KeycardRed:
                    if (player.keycardRed)
                    {
                        animator.SetBool("DoorOpen", true);
                        Invoke("CloseDoor", 5);
                    }
                    break;
                case KeycardDoors.Keycardpurple:
                    if (player.keycardPurple)
                    {
                        animator.SetBool("DoorOpen", true);
                        Invoke("CloseDoor", 5);
                    }
                    break;
                default:
                    break;
            }
        }
    }
    private void CloseDoor()
    {
        animator.SetBool("DoorOpen", false);
    }

}
