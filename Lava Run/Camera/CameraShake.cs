using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Threading;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float _shakeIntensity = 1f;

    private float _shakeTime;
    private CinemachineBasicMultiChannelPerlin _cbmcp;

    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Start()
    {
        StopShake();
    }

    /// <summary>
    /// Starts Shaking The Camera.
    /// </summary>
    private void ShakeCamera(float _timer)
    {
        _cbmcp.m_AmplitudeGain = _shakeIntensity;
        _shakeTime = _timer;
    }
    /// <summary>
    /// Stops Shaking The Camera.
    /// </summary>
    void StopShake()
    {
        _cbmcp.m_AmplitudeGain = 0f;
        _shakeTime = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShakeCamera(0.2f);
        }

        if (_shakeTime > 0)                 //Timer for how long to shake.
        {
            _shakeTime -= Time.deltaTime;
            if (_shakeTime <= 0)
            {
                StopShake();
            }
        }
    }
}
