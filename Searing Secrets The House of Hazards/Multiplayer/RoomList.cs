using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomList : MonoBehaviourPunCallbacks
{
    public GameObject RoomPrefab;
    public GameObject[] AllRooms;

    /// <summary>
    /// Updates the Room List with the current rooms.
    /// </summary>
    /// <param name="roomList"></param>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //Deletes the room form the list  if is no one active in the room.
        for (int i = 0; i < AllRooms.Length; i++)
        {
            if (AllRooms[i] != null)
            {
                Destroy(AllRooms[i]);
            }
        }

        AllRooms =  new GameObject[roomList.Count];

        //Adds the button for joining the room to the list.
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].IsOpen && roomList[i].IsVisible && roomList[i].PlayerCount >= 1)
            {
                Debug.Log(roomList[i].Name);
                GameObject _room = Instantiate(RoomPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Content").transform);
                _room.GetComponent<Room>().Name.text = roomList[i].Name;

                AllRooms[i] = _room;
            }
            
        }
    }
}
