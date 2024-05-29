using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float bulletTimer = 5f;
    public int bulletDamage;
    public int bulletSpeed;
    private void Start()
    {
        Destroy(gameObject, bulletTimer);
    }

    private void Update()
    {
        transform.Translate(0, 0, bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Player can take damage from enemy bullets.
        if (other.gameObject.GetComponent<PlayerHealth>())
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamagePlayer(bulletDamage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
