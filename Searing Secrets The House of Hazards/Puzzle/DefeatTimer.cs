using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class DefeatTimer : MonoBehaviourPunCallbacks
{
    private Timer _timer;

    private void Start()
    {
        _timer = new Timer();

        if (PhotonNetwork.IsMasterClient)
        {
            _timer.SetTimer(450);
        }
    }

    private void Update()
    {
        if (_timer.isActive && _timer.TimerDone())
        {
            _timer.StopTimer();
            Defeat();
        }
    }

    private void Defeat()
    {
        PhotonNetwork.LoadLevel("Defeat");
        photonView.RPC("DefeatOther", RpcTarget.Others);
    }

    [PunRPC]
    private void DefeatOther()
    {
        PhotonNetwork.LoadLevel("Defeat");
    }
}
