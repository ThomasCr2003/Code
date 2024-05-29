using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public interface IDamagable
{
    void TakeDamage(int damage);
}

public class Enemy : MonoBehaviour , IDamagable
{
    public int enemyHealth;
    public bool enemyBoss;
    #region Patrol,Chase and Shoot Variables
    [SerializeField] private GameObject[] _WayPoints;
    private int _currentWaypointIndex = 0;
    private float _speed = 5f;

    #endregion


    #region Vision Cone Variables

    [SerializeField] public bool playerDetected { get; private set; }

    public Vector2 directionToTarget => target.transform.position - detectorOrigin.position;

    [SerializeField] private Transform detectorOrigin;
    public Vector2 detectorSize = Vector2.one;
    public Vector2 detectorOriginOffset = Vector2.zero;

    public float detectionDelay = 0.3f;
    public LayerMask detectorLayerMask;

    public Color gizmoIdleColor = Color.green;
    public Color gizmodetectedColor = Color.red;
    public bool showGizmos = true;

    public GameObject target;
    #endregion

    public GameObject Target
    {
        get => target;
        private set
        {
            target = value;
            playerDetected = target != null;
        }
    }

    public virtual void Start()
    {
        Patrol();
        StartCoroutine(DetectionCoroutine());
    }

    public virtual void Update()
    {
        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    public void PerformDetection()
    {
        Collider2D collider = Physics2D.OverlapBox((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize, 0, detectorLayerMask);
        if (collider != null) 
        {
            Target = collider.gameObject;
        }
        else
        {
            Target = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = gizmoIdleColor;
            if (playerDetected)
            {
                Gizmos.color = gizmodetectedColor;
                Chase();
            }
            Gizmos.DrawCube((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize);

        }
    }

    

    public void Die()
    {
        if (enemyBoss)
        {
            SceneManager.LoadScene("EndScreen");
        }
        Destroy(gameObject);
    }
    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
    }

    #region Patrol and Chase Methods.
    /// <summary>
    /// Makes the enemy patrol between the checkpoints.
    /// </summary>
    private void Patrol()
    {
        // Calcuclates the distance between the checkpoint and the enemy.
        if (Vector3.Distance(_WayPoints[_currentWaypointIndex].transform.position, transform.position) < 0.1f)
        {
            // Makes the enemy return to the other waypoint.
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= _WayPoints.Length)
            {
                _currentWaypointIndex = 0;
            }

        }
        // Moves the enemy
        transform.position = Vector3.MoveTowards(transform.position, _WayPoints[_currentWaypointIndex].transform.position, Time.deltaTime * _speed);
    }
    /// <summary>
    /// Makes the enemy chase the player and starts shooting at it.
    /// </summary>
    public virtual void Chase()
    {
    }
    #endregion

}
