using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BigScream : MonoBehaviour, IAbility
{
    [SerializeField] private float _CooldownTimer;
    [SerializeField] private float _AddedRange;
    private float _oldRadius;
    private PlayerFieldOfView _pFOV;
    private bool _isOnCoolDown;

    [SerializeField] private Color _StandartColor;
    [SerializeField] private Color _ScreamColor;
    [SerializeField] private Material _ScreamMaterial;

    //CooldownStuff
    [SerializeField] private Image _Fade;
    [SerializeField] private TMP_Text _CooldownText;
    private float _cooldownTime;

    private void Start()
    {
        _pFOV = GetComponent<PlayerFieldOfView>();
        _StandartColor = _ScreamMaterial.GetColor("_EmissionColor");
    }

    private void Update()
    {
        if (_isOnCoolDown)
        {
            ApplyCooldown();
        }
    }

    public void Activate()
    {
        if (!_isOnCoolDown)
        {
            _oldRadius = _pFOV.GetPlayerRadius();
            _pFOV.SetPlayerRadius(_AddedRange + _pFOV.GetPlayerRadius());
            _isOnCoolDown = true;
            StartCoroutine(ResetRadius());
            _ScreamMaterial.SetColor("_EmissionColor", _ScreamColor);
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

    public IEnumerator ResetRadius()
    {
        yield return new WaitForSeconds(3);
        _ScreamMaterial.SetColor("_EmissionColor", _StandartColor);
        _pFOV.SetPlayerRadius(_oldRadius);
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
