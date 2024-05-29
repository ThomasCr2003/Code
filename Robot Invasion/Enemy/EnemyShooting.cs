using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform target;
    private float shootTimer;
    private bool hasShot;
    private float timer;
    public EnemyStats stats;

    private void Start()
    {
        stats = GetComponent<EnemyStats>();
    }
    /// <summary>
    /// Enemy Shooting
    /// </summary>
    public void EnemyShoot()
    {
        if (stats.enemyAmmo > 0 && !hasShot)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation); 
            bullet.GetComponent<EnemyBullet>().bulletSpeed = stats.bulletSpeed;
            bullet.GetComponent<EnemyBullet>().bulletDamage = stats.bulletDamage;
            bulletSpawnPoint.transform.position = new Vector3(bulletSpawnPoint.position.x, target.position.y, bulletSpawnPoint.position.z);
            stats.enemyAmmo--;
            hasShot = true;
        }
        else
        {
            ReloadEnemy();
        }
    }

    public void Update()
    {

        //Makes a fire rate for the enemy.
        if (hasShot)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer > stats.fireRate)
            {
                shootTimer = 0;
                hasShot = false;

            }
        }
    }
    /// <summary>
    /// Reloads enemy gun.
    /// </summary>
    private void ReloadEnemy()
    {
        if (stats.enemyAmmo <= 0)
        {
            timer += Time.deltaTime;
            if (timer >= stats.enemyReloadTimer)
            {
                stats.enemyAmmo = stats.enemyAmmoClip;
                timer = 0;
            }
        }
    }
}
