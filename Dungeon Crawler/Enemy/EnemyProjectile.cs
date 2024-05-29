using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int bulletSpeed;
    private float bulletTimer = 5f;
    public int bulletDamage;
    private EnemyGhost enemy;
    [SerializeField] private bool bossBullet;
    void Start()
    {
        Destroy(gameObject,bulletTimer);
        enemy = FindObjectOfType<EnemyGhost>();
    }

    
    void Update()
    {
        if (!bossBullet)
        {
            if (enemy.left)
            {
                transform.Translate(bulletSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.Translate(0, bulletSpeed * Time.deltaTime, 0);
            }

        }
        else if (bossBullet)
        {
            transform.Translate(0, bulletSpeed * Time.deltaTime, 0);
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();

        if (damagable != null)
        {
            damagable.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
        else if (damagable == null)
        {
            Destroy(gameObject);
        }
    }
}
