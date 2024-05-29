using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }
    //Variable For The Total Money That Is Saved.
    public int TotalMoney;
    //Variable For The Current Money Collected This Run.
    public int CurrentMoneyCollected;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
        DontDestroyOnLoad(this);

        CurrentMoneyCollected = 0;
        GetSavedMoney();
    }

    /// <summary>
    /// Adds The Amount of Money you pickup to the current amount.
    /// </summary>
    /// <param name="_amount"></param>
    public void AddMoney(int _amount)
    {
        CurrentMoneyCollected += _amount;
    }

    /// <summary>
    /// Saves the Total Money and Sets Current Money Back to 0.
    /// </summary>
    public void SaveMoney()
    {
        if (CurrentMoneyCollected > 0)
        {
            TotalMoney = PlayerPrefs.GetInt(PlayerPrefsDefinitions.Money);
            TotalMoney += CurrentMoneyCollected;
            CurrentMoneyCollected = 0;
            PlayerPrefs.SetInt(PlayerPrefsDefinitions.Money, TotalMoney);
        }
    }

    /// <summary>
    /// Gets The Total Money You Have.
    /// </summary>
    public void GetSavedMoney()
    {
        TotalMoney = PlayerPrefs.GetInt(PlayerPrefsDefinitions.Money);
    }

}
