using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class Grappling : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private KeyCode _SwingKey = KeyCode.Mouse0;

    [Header("Reference")]
    [SerializeField] private LineRenderer _Lr;
    [SerializeField] private Transform _GunTip, _Cam, _Player;
    [SerializeField] private LayerMask _WhatIsGrappleable;
    private Crosshair _crosshair;

    [Header("Swinging")]
    private float _maxSwingDistance = 25f;
    private Vector3 _swingPoint;
    private SpringJoint _joint;
    private Vector3 _currentGrapplePosition;

    [Header("AirControl")]
    [SerializeField] private Transform _Orientation;
    [SerializeField] private Rigidbody _Rb;
    [SerializeField] private float _HorizontalThrustForce;
    [SerializeField] private float _ForwardThrustForce;
    [SerializeField] private float _ExtendCableSpeed;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_SwingKey))
        {
            StartSwing();
        }
        if (Input.GetKeyUp(_SwingKey))
        {
            StopSwing();
        }

        if (_joint != null)
        {
            AirControl();
        }

        CrosshairColor();
    }
    private void LateUpdate()
    {
        DrawRope();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject crosshairObject = GameObject.FindGameObjectWithTag("Crosshair");
        if (crosshairObject != null)
        {
            _crosshair = crosshairObject.GetComponent<Crosshair>();
        }
    }

    private void CrosshairColor()
    {
        RaycastHit hit;
        if (Physics.Raycast(_Cam.position, _Cam.forward, out hit, _maxSwingDistance, _WhatIsGrappleable))
        {
            if (_crosshair != null)
                _crosshair.SetGreenCrosshair();
        }
        else
        {
            if (_crosshair != null)
                _crosshair.SetWhiteCrosshair();
        }
    }

    
    /// <summary>
    /// Chechs if everything is ready to start grappling.
    /// </summary>
    private void StartSwing() 
    {
        RaycastHit hit;
        if (Physics.Raycast(_Cam.position ,_Cam.forward, out hit, _maxSwingDistance, _WhatIsGrappleable))
        {
            _swingPoint = hit.point;
            _joint = _Player.gameObject.AddComponent<SpringJoint>();
            _joint.autoConfigureConnectedAnchor = false;
            _joint.connectedAnchor = _swingPoint;

            float distanceFromPoint = Vector3.Distance(_Player.position, _swingPoint);

            // The distance grapple will try to keep from grapple point
            _joint.maxDistance = distanceFromPoint * 0.8f;
            _joint.minDistance = distanceFromPoint * 0.25f;

            _joint.spring = 4.5f;
            _joint.damper = 7f;
            _joint.massScale = 4.5f;

            _Lr.positionCount = 2;
            _currentGrapplePosition = _GunTip.position;
        }
    }

    private void StopSwing() 
    {
        _Lr.positionCount = 0;
        Destroy(_joint);
    }

    private void DrawRope() 
    {
        if (!_joint) return;

        _currentGrapplePosition = Vector3.Lerp(_currentGrapplePosition, _swingPoint, Time.deltaTime * 8f);

        _Lr.SetPosition(0, _GunTip.position);
        _Lr.SetPosition(1, _swingPoint);
    }

    /// <summary>
    /// Gives you the ability to steer and have more control while grappling.
    /// </summary>
    private void AirControl() 
    {
        // Swing Right
        if (Input.GetKey(KeyCode.D))
        {
            _Rb.AddForce(_Orientation.right * _HorizontalThrustForce * Time.deltaTime);
        }
        // Swing Left 
        if (Input.GetKey(KeyCode.A))
        {
            _Rb.AddForce(-_Orientation.right * _HorizontalThrustForce * Time.deltaTime);
        }

        // Swing Forward
        if (Input.GetKey(KeyCode.W))
        {
            _Rb.AddForce(_Orientation.right * _ForwardThrustForce * Time.deltaTime);
        }

        // Shortens the cable
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 directionToPoint = _swingPoint - transform.position;
            _Rb.AddForce(directionToPoint.normalized * _ForwardThrustForce * Time.deltaTime);

            float distanceFromPoint = Vector3.Distance(transform.position, _swingPoint);

            _joint.maxDistance = distanceFromPoint * 0.8f;
            _joint.minDistance = distanceFromPoint * 0.25f;
        }

        // Extends the cable
        if (Input.GetKey(KeyCode.S))
        {
            float extendedDistanceFromPoint = Vector3.Distance(transform.position, _swingPoint) + _ExtendCableSpeed;

            _joint.maxDistance = extendedDistanceFromPoint * 0.8f;
            _joint.minDistance = extendedDistanceFromPoint * 0.25f;
        }
    }
}
