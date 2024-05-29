using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed;
    public float jumpForce;
    [SerializeField] private float _GroundDrag;
    [SerializeField] private float _JumpCooldown;
    [SerializeField] private float _AirMultiplier;
    private bool _readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public LayerMask WhatIsGround;
    public Rigidbody rb;
    [SerializeField] private float _PlayerHeight;
    [SerializeField] private Transform _Bottem;
    [SerializeField] private Transform _Orientation;
    private bool _grounded;
    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _moveDirection;
    private ClassManager classManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        _readyToJump = true;
        classManager = FindObjectOfType<ClassManager>();
        classManager.Styles();
        DontDestroyOnLoad(gameObject);
        rb.useGravity = false;
    }

    private void Update()
    {
        // Calculates on where the ground is.
        _grounded = Physics.Raycast(_Bottem.position, Vector3.down, _PlayerHeight,WhatIsGround);

        MyInput();
        SpeedControl();
        if (_grounded)
        {
            rb.drag = _GroundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        classManager = FindObjectOfType<ClassManager>();
    }

    private void MyInput() 
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && _readyToJump && _grounded)
        {
            _readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), _JumpCooldown);
        }
    }

    private void MovePlayer() 
    {
        _moveDirection = _Orientation.forward * _verticalInput + _Orientation.right * _horizontalInput;


        if (_grounded)
        {
            rb.AddForce(_moveDirection.normalized * movementSpeed * 10, ForceMode.Force); 
        }
        else if (!_grounded) 
        {
            rb.AddForce(_moveDirection.normalized * movementSpeed * 10 * _AirMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl() 
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > movementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump() 
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump() 
    {
        _readyToJump = true;
    }
}
