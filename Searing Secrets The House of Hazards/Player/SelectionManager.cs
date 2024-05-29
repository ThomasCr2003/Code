using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;

public enum ESide
{
    None,
    Navigator,
    Escapist,
}

public class SelectionManager : MonoBehaviourPunCallbacks
{
    public static SelectionManager Instance { get; private set; }

    [SerializeField] private ESide _ESide;
    public bool HasPickedSide;
    public bool LockButtonNavigator;
    public bool LockButtonEscapist;
    public bool EscapistReady;
    public bool NavigatorReady;

    [Header("Buttons")]
    public Button ButtonNone;
    public Button ButtonNavigator;
    public Button ButtonEscapist;

    public TextMeshProUGUI Text;

    public Timer _timer;

    private void Start()
    {
        Instance = this;
        _ESide = ESide.None;
        _timer = new Timer();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (_timer.isActive && _timer.TimerDone())
        {
            _timer.StopTimer();
            StartGame();
        }

        if (_timer.isActive)
        {
            Text.text = Mathf.RoundToInt(_timer.TimeLeft()).ToString();
        }
    }

    public void SideChosen(ESide chosen)
    {
        _ESide = chosen;
    }

    public ESide GetChosenSide()
    {
        return _ESide;
    }

    public void LockNavigator()
    {
        ButtonNavigator.interactable = false;
        StartCountDown();
    }

    public void LockEscapist()
    {
        ButtonEscapist.interactable = false;
        StartCountDown();
    }

    public void UnLockNavigator()
    {
        ButtonNavigator.interactable = true;
    }

    public void UnLockEscapist()
    {
        ButtonEscapist.interactable = true;
    }

    public void StartCountDown()
    {
        if (LockButtonNavigator && LockButtonEscapist)
        {
            _timer.SetTimer(5);
        }
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("Demo");
    }

    public int GetSpawnPointIndex()
    {
        return (int)_ESide - 1;
    }
}
