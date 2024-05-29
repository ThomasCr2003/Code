using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable 
{
    void Interactable(GameObject Pos);

    Animation GetDoorAnimation();
    bool DoorHasNotBeenOpend();
}
