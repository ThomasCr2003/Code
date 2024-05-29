using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMoney : MonoBehaviour
{
    private float _timer;
    [SerializeField] private DepositMoneySound _DepositMoneySound;

    private void OnTriggerEnter(Collider other)
    {
        if (MoneyManager.Instance.CurrentMoneyCollected > 0)
        {
            //Saves the Money that you had.
            MoneyManager.Instance.SaveMoney();
            _DepositMoneySound.PlayDepositMoneyAudio();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //To Escape
        if (Input.GetKey(KeyCode.F))
        {
            GameManager.Instance.Victory();
        }
    }
}
