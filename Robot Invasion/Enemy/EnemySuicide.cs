using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuicide : MonoBehaviour
{
    public EnemyStats stats;

    private void Start()
    {
        stats = GetComponent<EnemyStats>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>())
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamagePlayer(stats.bulletDamage);
            GetComponent<Enemy>().enemyDead = true;
            gameObject.SetActive(false);
        }
    }
}
