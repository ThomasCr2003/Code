using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    public Animation DoorAnim;
    private bool hasbeenOpend;

    public void Interactable(GameObject pos)
    {

    }

    public Animation GetDoorAnimation()
    {
        hasbeenOpend = true;
        return DoorAnim;
    }

    public bool DoorHasNotBeenOpend()
    {
        return hasbeenOpend;
    }
}
