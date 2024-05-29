using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulcanoTrigger : MonoBehaviour
{
    [SerializeField] private CameraSwap _camera;
    private void OnTriggerEnter(Collider other)
    {
        var col = GetComponent<BoxCollider>();
        col.enabled = false;    
        _camera.pState = PlayerState.cutSceneVulcano;
    }
}
