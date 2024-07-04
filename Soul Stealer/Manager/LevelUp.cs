using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    public static LevelUp Instance { get; private set; }
    private int _level = 1;

    [Header("Selection")] public GameObject SelectionPanel;

    private void Awake(){
        if (Instance != null)
        {
            return;
        }
        else
        {
            Instance = this;
        }
    }

    public void LevelUpPlayer(){
        _level++;
        CalculateAbilityOrStatChange();
    }


    private void CalculateAbilityOrStatChange(){
        //Checks if you currently are on an even or uneven level to determine if you should chose another ability or stat change.
        if (_level % 2 == 0)
        {
            ChooseStatChange();
        }
        else if (_level % 2 == 1)
        {
            ChooseAbility();
        }
    }

    private void ChooseAbility(){
        if (!AbilityManager.Instance.AllAbilitiesChosen)
        {
            SelectionPanel.SetActive(true);
            Time.timeScale = 0;
            AbilityManager.Instance.ChooseAbilities();
        }
        else
        {
            ChooseStatChange();
        }
    }

    private void ChooseStatChange(){
        SelectionPanel.SetActive(true);
        Time.timeScale = 0;
        AbilityManager.Instance.ChooseStatChange();
    }

    public void Continue(){
        SelectionPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ResetPlayerLevel(){
        _level = 1;
    }


    private void Update(){
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F))
        {
            LevelUpPlayer();
        }
#endif
    }
}