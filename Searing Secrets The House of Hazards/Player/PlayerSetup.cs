using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] private Movement _Movement;
    [SerializeField] private GameObject _Camera;
    [SerializeField] private Canvas _Canvas;

    /// <summary>
    /// Activates the Player movement and camera for the player that you are playing with.
    /// </summary>
    public void IsLocalPlayer()
    {
        _Camera.SetActive(true);
        _Canvas.enabled = true;
        _Movement.enabled = true;
    }

}
