using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypointfollower : MonoBehaviour
{
    [SerializeField] private GameObject[] _WayPoints;
    private int _currentWaypointIndex = 0;
    private float _speed = 5f;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MovePlatform();
        }
    }
    
    private void MovePlatform() 
    {
        // Calcuclates the distance between the checkpoint and the platform.
        if (Vector3.Distance(_WayPoints[_currentWaypointIndex].transform.position, transform.position) < 0.1f) 
        {
            // Makes the platform return to the other platform.
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= _WayPoints.Length)
            {
                _currentWaypointIndex = 0;
            }

        }
        // Moves the platform
        transform.position = Vector3.MoveTowards(transform.position, _WayPoints[_currentWaypointIndex].transform.position, Time.deltaTime * _speed); 
    }
}
