using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class Puzzle1 : MonoBehaviourPunCallbacks
{
    public int RandomNumber;
    public int CurrentNumberCorrect;
    public int MaxNumbers;
    public bool HasBeenCompleted;
    public int synchnumber;
    public RawImage Image;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            RandomizeNumber();
        }
    }

    
    public void ButtonPressed(int Color)
    {
        if (!HasBeenCompleted && PhotonNetwork.IsMasterClient)
        {
            if (Color == RandomNumber)
            {
                CurrentNumberCorrect++;
                CheckIfDone();
                RandomizeNumber();
            }
            else
            {
                CurrentNumberCorrect = 0;
                RandomizeNumber();
            }
        }
        else if (!HasBeenCompleted && !PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("SynchButtonPress", RpcTarget.MasterClient, Color);
        }
    }

    [PunRPC]
    public void SynchButtonPress(int Color)
    {
        if (Color == RandomNumber)
        {
            CurrentNumberCorrect++;
            CheckIfDone();
            RandomizeNumber();
        }
        else
        {
            CurrentNumberCorrect = 0;
            RandomizeNumber();
        }
    }

    public void CheckIfDone()
    {
        if (CurrentNumberCorrect == MaxNumbers)
        {
            CurrentNumberCorrect++;
            photonView.RPC("DisableScreen", RpcTarget.Others);
            DisableScreen();
            HasBeenCompleted = true;
            PuzzleManager.puzzleManager.FirstPuzzleCompleted();
        }
    }

    public void RandomizeNumber()
    {
        if (!HasBeenCompleted && PhotonNetwork.IsMasterClient)
        {
            RandomNumber = Random.Range(0, 4);
            photonView.RPC("SynchNumber", RpcTarget.Others , RandomNumber);
            synchnumber = RandomNumber;
            ChangeScreenColor(synchnumber);
        }
    }

    [PunRPC]
    public void SynchNumber(int number)
    {
        synchnumber = number;
        ChangeScreenColor(synchnumber);
    }

    public int GetSychNumber()
    {
        return synchnumber; 
    }

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

    [PunRPC]
    public void DisableScreen()
    {
        Image.enabled = false;
    }
}
