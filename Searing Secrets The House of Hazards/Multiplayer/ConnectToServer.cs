using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{

    /// <summary>
    /// Connects you to the server.
    /// </summary>
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting....");
    }

    /// <summary>
    /// If you are connected to the server join the main lobby.
    /// </summary>
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("Joining Lobby");
    }

    /// <summary>
    /// Join the lobby.
    /// </summary>
    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
