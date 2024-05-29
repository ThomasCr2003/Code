using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Movement : MonoBehaviour
{
    public float Movespeed;
    public float MaxVelocityChange;

    private Vector2 _input;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _input.Normalize();
    }

    private void FixedUpdate()
    {
        if (GetComponent<PhotonView>().IsMine == true)
        {
            _rb.AddForce(CalculateMovement(Movespeed), ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Calculates the current movement speed of the player.
    /// </summary>
    /// <param name="_speed"></param>
    /// <returns></returns>
    private Vector3 CalculateMovement(float _speed)
    {
        Vector3 targetVelocity = new Vector3(_input.x, 0, _input.y);
        targetVelocity = transform.TransformDirection(targetVelocity);

        targetVelocity *= _speed;
        Vector3 velocity = _rb.velocity;

        if (_input.magnitude > 0.5)
        {
            Vector3 velocityChange = targetVelocity - velocity;

            velocityChange.x = Mathf.Clamp(velocityChange.x, -MaxVelocityChange, MaxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -MaxVelocityChange, MaxVelocityChange);

            velocityChange.y = 0;

            return velocityChange;
        }
        else
        {
            return new Vector3();
        }
    }
}
