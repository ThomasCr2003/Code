using Health;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifeSteal : MonoBehaviour, IAbility
{
    [SerializeField] private float _CooldownTimer;
    [SerializeField] private float _Duration;
    [SerializeField] private float _Amount;
    private bool _isOnCoolDown;
    private bool _LifeStealActive;

    [SerializeField] private HealthSystem _HealthSystem;
    [SerializeField] private Color _StandartColor;
    [SerializeField] private Color _LifeStealColor;
    [SerializeField] private Material _ScreamMaterial;

    //CooldownStuff
    [SerializeField] private Image _Fade;
    [SerializeField] private TMP_Text _CooldownText;
    private float _cooldownTime;

    private void Start()
    {
        _HealthSystem = GetComponent<HealthSystem>();
        _StandartColor = _ScreamMaterial.GetColor("_EmissionColor");
    }

    private void Update()
    {
        if (_isOnCoolDown)
        {
            ApplyCooldown();
        }
        while (_LifeStealActive)
        {
            _HealthSystem.SetLifeSteal(_Amount);
            break;
        }
    }

    public void Activate()
    {
        if (!_isOnCoolDown)
        {
            _isOnCoolDown = true;
            StartCoroutine(StartLifeStealTimer(_Duration));
            _CooldownText.gameObject.SetActive(true);
            _cooldownTime = _CooldownTimer;
            _CooldownText.text = Mathf.RoundToInt(_cooldownTime).ToString();
            _Fade.fillAmount = 1.0f;

        }
        else
        {
            Debug.Log("Ability is Not Ready Yet.");
        }
    }

    public IEnumerator StartLifeStealTimer(float _amount)
    {
        _LifeStealActive = true;
        _ScreamMaterial.SetColor("_EmissionColor" , _LifeStealColor);
        yield return new WaitForSeconds(_amount);
        _HealthSystem.SetLifeSteal(0);
        _ScreamMaterial.SetColor("_EmissionColor", _StandartColor);
        _LifeStealActive = false;
    }

    public void ApplyCooldown()
    {
        _cooldownTime -= Time.deltaTime;
        if (_cooldownTime < 0.0f)
        {
            _isOnCoolDown = false;
            _CooldownText.gameObject.SetActive(false);
            _Fade.fillAmount = 0.0f;
        }
        else
        {
            _CooldownText.text = Mathf.RoundToInt(_cooldownTime).ToString();
            _Fade.fillAmount = _cooldownTime / _CooldownTimer;

        }
    }

    public void SetIconStuff(Image fade, TMP_Text text)
    {
        _Fade = fade;
        _CooldownText = text;
    }
}
