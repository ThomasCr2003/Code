using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStats stats;
    public bool enemyDead;
    public Animator enemyAnimator;

    public virtual void Start()
    {
        enemyDead = false;
        stats = GetComponent<EnemyStats>();
        GameManager.instance.enemies.Add(this);
        enemyAnimator = GetComponentInParent<Animator>();
    }

    public virtual void Update()
    {
        if (stats.enemyHealth == 0)
        {
            Die();
        }
    }
    /// <summary>
    /// Enemy can die.
    /// </summary>
    private void Die()
    {
        enemyDead = true;
        gameObject.SetActive(false);
    }
    public void EnemyTakeDamage(int amount)
    {
        stats.enemyHealth -= amount;
    }
}
