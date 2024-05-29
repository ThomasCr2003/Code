using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviourPunCallbacks
{
    public Animation DoorAnimation;
    private bool AnimationHasPlayed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End"))
        {
            Victory();
        }
    }

    [PunRPC]
    public void Victory()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("VictoryOther", RpcTarget.Others);
            PhotonNetwork.LoadLevel("Victory");
        }
        else
        {
            SynchVictory();
        }
    }

    public void SynchVictory()
    {
        photonView.RPC("Victory", RpcTarget.MasterClient);
    }

    [PunRPC]
    public void VictoryOther()
    {
        PhotonNetwork.LoadLevel("Victory");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Door") && !AnimationHasPlayed && DoorAnimation != null)
        {
            DoorAnimation.Play();
            AnimationHasPlayed = true;
        }
    }
}
