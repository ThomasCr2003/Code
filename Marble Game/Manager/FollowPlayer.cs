using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _PlayerTransform;

    private void Update()
    {
        transform.position = _PlayerTransform.position;
    }
}
