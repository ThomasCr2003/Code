using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviourPunCallbacks
{
    public GameObject Player;
    public Transform[] SpawnPoint;
    public int Index;

    /// <summary>
    /// Spawns the player on the given Position.
    /// </summary>
    private void Start()
    {
        Index = SelectionManager.Instance.GetSpawnPointIndex();
        GameObject _player = PhotonNetwork.Instantiate(Player.name, SpawnPoint[Index].position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().IsLocalPlayer();
    }
}
