using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDoor : MonoBehaviour
{
    private Animator animator;
    public int orderdoor;

    private void Start()
    {
        animator = gameObject.transform.parent.parent.GetComponent<Animator>();
        GameManager.instance.AddDoors(orderdoor,this.gameObject);
    }

    public void OpenDoor()
    {
        animator.SetBool("DoorOpen", true);
    }

}
