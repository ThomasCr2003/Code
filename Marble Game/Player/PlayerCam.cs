using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    public float RotationSpeed = 1;
    public Transform Target, Player, Orientation;
    private float _mouseX, _mouseY;

    private void LateUpdate()
    {
        if (Input.GetMouseButton(1) && GameManager.Instance.canMoveCam) // Right Mouse Button
        {
            CamControl();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void CamControl()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        _mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;
        _mouseY = Mathf.Clamp(_mouseY, -50, 40);

        transform.LookAt(Target);

        Target.rotation = Quaternion.Euler(_mouseY, _mouseX, 0);
        Orientation.rotation = Quaternion.Euler(0, _mouseX, 0); 
    }
}
