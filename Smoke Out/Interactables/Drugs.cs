using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Drugs : MonoBehaviour , IInteratable
{
    [SerializeField] private int _MoneyWorth;
    public void Interact()
    {
        MoneyManager.Instance.AddMoney(_MoneyWorth);
        Destroy(gameObject);
    }

}
