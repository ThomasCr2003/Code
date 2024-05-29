using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ScreenFlash : MonoBehaviourPunCallbacks
{
    public RawImage Image;
    public Puzzle1 puzzle1;

    public void SynchChangeScreenColor(int Number)
    {
        photonView.RPC("ChangeScreenColor", RpcTarget.Others, Number);
    }

    [PunRPC]
    public void ChangeScreenColor(int Number)
    {
        if (Number == 0)
        {
            Image.color = Color.green;
        }
        if (Number == 1)
        {
            Image.color = Color.red;
        }
        if (Number == 2)
        {
            Image.color = Color.blue;
        }
        if (Number == 3)
        {
            Image.color = Color.yellow;
        }
    }
}
