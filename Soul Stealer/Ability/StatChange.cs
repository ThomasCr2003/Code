using Health;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatChange : MonoBehaviour
{
    [SerializeField] private PlayerFieldOfView _pFOV;
    [SerializeField] private EStatChanges _Stats;
    [SerializeField] private HealthSystem _healthSystem;

    private void Start()
    {
        _pFOV = AbilityManager.Instance._player.GetComponent<PlayerFieldOfView>();
        _healthSystem = AbilityManager.Instance._player.GetComponent<HealthSystem>();
    }

    private void Update()
    {
        if (AbilityManager.Instance.CardHasBeenPicked)
        {
            LevelUp.Instance.Continue();
            Destroy(gameObject);
        }
    }
    public void IncreaseFOV(float amount)
    {
        _pFOV.SetPlayerRadius(amount + _pFOV.GetPlayerRadius());
    }

    public void OnPress(float _Amount)
    {
        AbilityManager.Instance.CardHasBeenPicked = true;
        switch (_Stats)
        {
            case EStatChanges.Range:
                IncreaseFOV(_Amount);
                break;
            case EStatChanges.Damage:
                _healthSystem.SetDamage(_Amount);
                break;
            case EStatChanges.HealthIncrease:
                _healthSystem.IncreaseMaximunHealth(_Amount);
                break;
            default:
                break;
        }
    }
}
