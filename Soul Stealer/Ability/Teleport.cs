using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Teleport : MonoBehaviour, IAbility
{
    public Transform TeleportLocation;
    [SerializeField] private Transform TeleportLocationHolder;
    [SerializeField] private float _CooldownTime;
    private bool _isOnCoolDown;

    //BoxRaycastStuff
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _layerMask;

    //CooldownStuff
    [SerializeField] private Image _Fade;
    [SerializeField] private TMP_Text _CooldownText;
    private float _CooldownTimer;

    [SerializeField] private GameObject _TeleportStuff;
    private bool _visualActive;

    private void Start()
    {
        TeleportLocationHolder.position = TeleportLocation.position;
    }
    private void Update()
    {
        if (_isOnCoolDown)
        {
            ApplyCooldown();
        }
    }

    private void FixedUpdate()
    {
        //Checks if their is an object between the Teleport Location And the player. If So it changes the teleport position.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance, _layerMask))
        {
            TeleportLocation.position = hit.point;
        }
        else
        {
            TeleportLocation.position = TeleportLocationHolder.position;
        }
    }

    /// <summary>
    /// Activate The Ability.
    /// </summary>
    public void Activate()
    {
        if (!_isOnCoolDown)
        {
            if (_visualActive)
            {
                transform.position = TeleportLocation.position;
                _isOnCoolDown = true;

                _CooldownText.gameObject.SetActive(true);
                _CooldownTimer = _CooldownTime;
                _CooldownText.text = Mathf.RoundToInt(_CooldownTimer).ToString();
                _Fade.fillAmount = 1.0f;
                _visualActive = false;
                _TeleportStuff.SetActive(false);
            }
            else
            {
                _TeleportStuff.SetActive(true);
                _visualActive = true;
            }
        }
        else
        {
            Debug.Log("Ability is Not Ready Yet.");
        }
    }

    public void ApplyCooldown()
    {
        _CooldownTimer -= Time.deltaTime;
        if (_CooldownTimer < 0.0f)
        {
            _isOnCoolDown = false;
            _CooldownText.gameObject.SetActive(false);
            _Fade.fillAmount = 0.0f;
        }
        else
        {
            _CooldownText.text = Mathf.RoundToInt(_CooldownTimer).ToString();
            _Fade.fillAmount = _CooldownTimer / _CooldownTime;

        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * _maxDistance);
    }

    public void SetIconStuff(Image fade, TMP_Text text)
    {
        _Fade = fade;
        _CooldownText = text;
    }
}
