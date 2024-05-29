using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TeamSelection : MonoBehaviourPunCallbacks
{
    public ESide _Side;

    public void OnButtonPress()
    {
        if (!SelectionManager.Instance.HasPickedSide)
        {
            photonView.RPC("StartLockCheck", RpcTarget.All);
            SelectionLogic();
        }
        else if (SelectionManager.Instance.HasPickedSide && _Side == ESide.None)
        {
            photonView.RPC("UnLock", RpcTarget.All);
        }
    }

    [PunRPC]
    public void StartLockCheck()
    {
        StartCoroutine("LockButtonsCheck");
    }


    public void SelectionLogic()
    {
        SelectionManager.Instance.SideChosen(_Side);
        SelectionManager.Instance.HasPickedSide = true;
    }

    public IEnumerator LockButtonsCheck()
    {
        LockCheck();
        yield return null;
    }

    public void LockCheck()
    {
        SelectionManager.Instance.GetChosenSide();
        switch (_Side)
        {
            case ESide.None:
                SelectionManager.Instance.HasPickedSide = false;
                UnLock();
                break;
            case ESide.Navigator:
                SelectionManager.Instance.LockButtonNavigator = true;
                SelectionManager.Instance.LockNavigator();
                break;
            case ESide.Escapist:
                SelectionManager.Instance.LockButtonEscapist = true;
                SelectionManager.Instance.LockEscapist();
                break;
            default:
                break;
        }
    }

    [PunRPC]
    public void UnLock()
    {
        SelectionManager.Instance.SideChosen(_Side);
        SelectionManager.Instance.LockButtonNavigator = false;
        SelectionManager.Instance.UnLockNavigator();
        SelectionManager.Instance.LockButtonEscapist = false;
        SelectionManager.Instance.UnLockEscapist();
        SelectionManager.Instance.HasPickedSide = false;
        SelectionManager.Instance._timer.StopTimer();
        SelectionManager.Instance.Text.text = "";
    }

}

