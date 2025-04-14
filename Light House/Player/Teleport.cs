using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public Transform PlayerTransform;
    private CharacterController controller;
    private bool teleported;
    private float teleportWait = 0.70f;
    private bool canTeleport;
    [SerializeField] private float _Cooldown;
    [SerializeField] private float _TeleportDistance;
    private AudioSource snap;
    [SerializeField] private Image _SnapImage;
    [SerializeField] private Animator _animator;
    [SerializeField] private Sanity _sanity;
    [SerializeField] private Animator _ChangeRingColor;
    [SerializeField] private AmbientSound _Ambient;

    [Header("Camera")] 
    [SerializeField] private Camera _Camera;
    [SerializeField] private float _CameraShakeAmount;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        canTeleport = false;
        snap = GetComponent<AudioSource>();
        PlayerTransform = GetComponent<Transform>();
        _Camera = GetComponentInChildren<Camera>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && canTeleport)
        {
            StartCoroutine("teleport");
        }
    }

    IEnumerator teleport()
    {
        canTeleport = false;
        _animator.SetBool("Snap", true);
        snap.Play();
        yield return new WaitForSeconds(teleportWait);
        if (!teleported)
        {
            controller.enabled = false;
            Vector3 newpos = new Vector3(PlayerTransform.position.x /*+ _TeleportDistance*/, PlayerTransform.transform.position.y + _TeleportDistance, PlayerTransform.transform.position.z);
            PlayerTransform.position = newpos;
            //StartCoroutine("ShakeCamera");
            _Ambient.PlayShadowAmbient();
            _ChangeRingColor.SetBool("ShadowWorld",true);
            _sanity.ShadowDimension = true;
            _sanity.RandomizeWhisper();
            controller.enabled = true;
            teleported = true;  
        }
        else 
        {
            _sanity.ShadowDimension = false;
            _sanity.ResetTimer();
            controller.enabled = false;
            Vector3 newpos = new Vector3(PlayerTransform.transform.position.x /*- _TeleportDistance */, PlayerTransform.transform.position.y - _TeleportDistance, PlayerTransform.transform.position.z);
            PlayerTransform.position = newpos;
            //StartCoroutine("ShakeCamera");
            _Ambient.PlayNormalAmbient();
            _ChangeRingColor.SetBool("ShadowWorld", false);
            _sanity.TurnOffWhisper();
            controller.enabled = true;
            teleported = false;
        }
        _animator.SetBool("Snap",false);
        yield return new WaitForSeconds(_Cooldown);
        canTeleport = true;
    }

    //IEnumerator ShakeCamera()
    //{
    //    Vector3 NewCameraPos = new Vector3 (_Camera.transform.position.x + _CameraShakeAmount ,_Camera.transform.position.y, _Camera.transform.position.z);
    //    Vector3 CameraPosition = _Camera.transform.position;
    //    _Camera.transform.position = NewCameraPos;
    //    yield return new WaitForSeconds(0.5f);
    //    _Camera.transform.position = CameraPosition;
    //}

    public void SetTeleport(bool state)
    {
        canTeleport = state;
    }
}
