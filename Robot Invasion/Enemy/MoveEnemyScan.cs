using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MoveEnemyScan : Enemy
{
    #region Patrol,Chase and Shoot Variables
    public Transform[] enemyDestination;
    private int destPoint;
    private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [SerializeField] private PlayerHealth pHealth;
    private EnemyShooting enemyShooting;
    
    #endregion
    #region Vision Cone Variables

    public float distance = 10;
    public float angle = 30;
    public float height = 1.0f;
    public Color meshColor = Color.red;
    public int scanFrequency = 30;
    public int segments;

    public LayerMask layers;
    public LayerMask occlusionLayers;
    public List<GameObject> Objects = new List<GameObject>();
    private Collider[] colliders = new Collider[50];
    private int count;
    private float scanInterval;
    private float scanTimer;

    Mesh mesh;
    #endregion
    #region Start and Update.
    public override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        if (stats.enemySuicide)
        {
            agent.speed = stats.enemySpeed;
        }        
        agent.destination = enemyDestination[0].position;
        //Prevents the ai from slowing down when reaching their checkpoint.
        agent.autoBraking = false;
        enemyShooting = GetComponent<EnemyShooting>();
        Patrol();   
    }
    
    public override void Update()
    {
        base.Update();
        Scan();
        if (Objects.Count <= 0)
        {
            Patrol();
        }
    }
    #endregion
    #region Patrol and Chase Methods.
    /// <summary>
    /// Calculates the next checkpoint he should move to.
    /// </summary>
    private void DestinationCheckPoints()
    {
        if (enemyDestination.Length == 0)
        {
            return;
        }

        agent.destination = enemyDestination[destPoint].position;

        destPoint = (destPoint + 1) % enemyDestination.Length;
    }
    /// <summary>
    /// Makes the enemy patrol between the checkpoints.
    /// </summary>
    private void Patrol()
    {
        // Choose the next destination point when the agent gets close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f && stats.needsAnimator)
        {
            enemyAnimator.ResetTrigger("Idle");
            enemyAnimator.ResetTrigger("Shooting");
            enemyAnimator.SetTrigger("Walking");
            DestinationCheckPoints();
        }
        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            DestinationCheckPoints();
        }
    }
    /// <summary>
    /// Makes the enemy chase the player and starts shooting at it.
    /// </summary>
    private void Chase()
    {
        if (!stats.enemySuicide && !stats.enemySniper && !stats.enemyBase)
        {
            enemyShooting.EnemyShoot();
            transform.LookAt(target.position);
            agent.SetDestination(transform.position);
        }
        else if (stats.enemySuicide)
        {
            transform.LookAt(target.position);
            agent.SetDestination(target.position);
        }
        else if (stats.enemySniper)
        {
            enemyShooting.EnemyShoot();
            transform.LookAt(target.position);
            agent.SetDestination(transform.position);
            pHealth.Invoke("AlertFromSniper", 0f);
        }
        else if (stats.enemyBase)
        {
            enemyAnimator.ResetTrigger("Idle");
            enemyAnimator.ResetTrigger("Walking");
            enemyAnimator.SetTrigger("Shooting");
            enemyShooting.EnemyShoot();
            transform.LookAt(target.position);
            agent.SetDestination(transform.position);
        }
    }
    #endregion
    #region Vision Cone
    /// <summary>
    /// Logic behind the visable sphere.
    /// </summary>
    /// <returns></returns>
    Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();

        int numTriangles = (segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;

        int vert = 0;

        //left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        //right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;
        for (int i = 0; i < segments; i++)
        {

            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * distance;

            topRight = bottomRight + Vector3.up * height;
            topLeft = bottomLeft + Vector3.up * height;

            //far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            //top
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            //bottom

            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            currentAngle += deltaAngle;
        }

        for (int i = 0; i < numVertices; i++)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
    private void OnValidate()
    {
        mesh = CreateWedgeMesh();
        scanInterval = 1.0f / scanFrequency;
    }

    /// <summary>
    /// Draws the Sphere and makes the scan visable in the scene.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (mesh)
        {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }

        Gizmos.DrawWireSphere(transform.position, distance);
        for (int i = 0; i < count; i++)
        {
            Gizmos.DrawSphere(colliders[i].transform.position, 1f);
        }
        Gizmos.color = Color.green;
        foreach (var obj in Objects)
        {
            Gizmos.DrawSphere(obj.transform.position, 1f);
        }
    }

    /// <summary>
    /// Checks if any nonscannable layer is in the way or other reasons the scan wouldnt work.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool IsInSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;
        if (direction.y < -.3f || direction.y > height)
        {
            return false;
        }
        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if (deltaAngle > angle)
        {
            return false;
        }

        origin.y += height / 2;
        dest.y = origin.y;
        if (Physics.Linecast(origin, dest, occlusionLayers))
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Scans for the player.
    /// </summary>
    public void Scan()
    {
        count = Physics.OverlapSphereNonAlloc(transform.position, distance, colliders, layers, QueryTriggerInteraction.Collide);
        Objects.Clear();
        for (int i = 0; i < count; i++)
        {
            GameObject obj = colliders[i].gameObject;
            if (IsInSight(obj))
            {
                Objects.Add(obj);
                Chase();
            }
        }
    }
    #endregion
}
