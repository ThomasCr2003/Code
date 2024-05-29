using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PuzzleManager : MonoBehaviourPunCallbacks
{
    public  static PuzzleManager puzzleManager { get; private set; }

    public bool FirstPuzzleDone;
    public bool SecondPuzzleDone;
    public bool LastPuzzleDone;

    public bool SecondPuzzleP1Done;
    public bool SecondPuzzleP2Done;
    public bool SecondPuzzleP3Done;

    public Animation DoorAnimation;
    public Animation DrawerAnimation;

    public AudioSource Door;
    public AudioSource Drawer;


    private void Start()
    {
        if (puzzleManager != null)
        {
            return;
        }
        else
        {
            puzzleManager = this;
        }
        
    }

    [PunRPC]
    public void FirstPuzzleCompleted()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            FirstPuzzleDone = true;
            DoorAnimation.Play();
            Door.Play();
            photonView.RPC("SynchDoorOpen", RpcTarget.Others);
        }
        else if (!PhotonNetwork.IsMasterClient)
        {
            SynchFirstPuzzleCompleted();
        }
    }

    
    public void SynchFirstPuzzleCompleted()
    {
        photonView.RPC("FirstPuzzleCompleted", RpcTarget.MasterClient);
    }

    [PunRPC]
    public void SynchDoorOpen()
    {
        DoorAnimation.Play();
    }

    public void CheckIfSecondPuzzleCompleted()
    {
        if (SecondPuzzleP1Done && SecondPuzzleP2Done && SecondPuzzleP3Done)
        {
            SecondPuzzleDone = true;
            DrawerAnimation.Play();
            Drawer.Play();
        }
    }
}
