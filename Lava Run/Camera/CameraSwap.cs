using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerState
{
    alive,
    dead,
    cutSceneVulcano,
    cutSceneFinalJump,
}
public class CameraSwap : MonoBehaviour
{
    public GameObject[] cams;
    public PlayerState pState;
    public bool vulcanoCutScene;
    public bool playerDead;
    [SerializeField] private Car _car;

    private void Update()
    {
        
        switch (pState)
        {
            case PlayerState.alive:
                vulcanoCutScene = false;
                cams[0].SetActive(true);
                break;
            case PlayerState.dead:
                playerDead = true;
                cams[0].SetActive(false);
                cams[1].SetActive(true);
                _car.Power = 0;
                break;
            case PlayerState.cutSceneVulcano:
                vulcanoCutScene = true;
                GameManager.instance.StartRockThrowTimer(1);
                cams[0].SetActive(false);
                cams[1].SetActive(false);
                cams[2].SetActive(true);
                break;
            case PlayerState.cutSceneFinalJump:
                cams[0].SetActive(false);
                cams[1].SetActive(false);
                cams[2].SetActive(false);
                cams[3].SetActive(true);
                break;
            default:
                break;
        }

    }
}
