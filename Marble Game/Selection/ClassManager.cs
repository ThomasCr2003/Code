using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Schema;
using System;


public enum EStyleType
{
    Mantis,
    Sonic,
    Hooker,
    NONE,
}
public class ClassManager : MonoBehaviour
{
    public EStyleType styleChosen;
    private PlayerMovement _playerMovement;
    private Grappling _grappling;
    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _grappling = FindObjectOfType<Grappling>();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        styleChosen = EStyleType.NONE;
    }


    /// <summary>
    /// Sets the style type correct.
    /// </summary>
    /// <param name="chosen"></param>
    public void StyleChosen(EStyleType chosen)
    {
        styleChosen = chosen;
        Styles();
    }

    /// <summary>
    /// Depends on which style is chosen, sets the correct value's for the player.
    /// </summary>
    public void Styles()
    {
        switch (styleChosen)
        {
            case EStyleType.Mantis:
                _playerMovement.jumpForce = 18;
                _playerMovement.movementSpeed = 7;
                _grappling.enabled = false;
                break;
            case EStyleType.Sonic:
                _playerMovement.movementSpeed = 14;
                _playerMovement.jumpForce = 13;
                _grappling.enabled = false;
                break;
            case EStyleType.Hooker:
                _playerMovement.jumpForce = 12;
                _playerMovement.movementSpeed = 7;
                _grappling.enabled = true;
                break;
            case EStyleType.NONE:
                _playerMovement.jumpForce = 13;
                _playerMovement.movementSpeed = 7;
                _grappling.enabled = false;
                break;
            default:
                break;
        }
    }
}

