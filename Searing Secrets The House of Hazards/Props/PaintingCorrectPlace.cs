using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PaintingCorrectPlace : MonoBehaviourPunCallbacks
{
    public int PaintingID;

    public GameObject GetPosition()
    {
        return gameObject;
    }

    [PunRPC]
    public void CheckIfRightPainting(int ID)
    {
        if (ID == PaintingID && PhotonNetwork.IsMasterClient)
        {
            if (PaintingID == 1)
            {
                PuzzleManager.puzzleManager.SecondPuzzleP1Done = true;
            }
            if (PaintingID == 2)
            {
                PuzzleManager.puzzleManager.SecondPuzzleP2Done = true;
            }
            if (PaintingID == 3)
            {
                PuzzleManager.puzzleManager.SecondPuzzleP3Done = true;
            }
            PuzzleManager.puzzleManager.CheckIfSecondPuzzleCompleted();
        }
        else if (!PhotonNetwork.IsMasterClient)
        {
            SynchCheckIfRightPainting(ID);
        }
    }

    public void SynchCheckIfRightPainting(int ID)
    {
        photonView.RPC("CheckIfRightPainting", RpcTarget.MasterClient, ID);
    }
}
