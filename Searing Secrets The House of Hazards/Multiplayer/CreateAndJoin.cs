using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    public TMP_InputField Input_Create;
    public TMP_InputField Input_Join;

    /// <summary>
    /// Create Room With Max of 2 players.
    /// </summary>
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(Input_Create.text, new RoomOptions() { MaxPlayers = 2, IsVisible = true, IsOpen = true }, TypedLobby.Default, null);
    }

    /// <summary>
    /// Join the room which has the given name.
    /// </summary>
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(Input_Join.text);
    }

    /// <summary>
    /// Join a Room that is in the current list.
    /// </summary>
    /// <param name="_roomName"></param>
    public void JoinRoomInList(string _roomName)
    {
        PhotonNetwork.JoinRoom(_roomName);
    }

    /// <summary>
    /// If you joined a room you will go to next scene.
    /// </summary>
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("TeamSelection");
    }
}
