using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhost : Enemy
{
    [Header("Shooting")]
    [SerializeField] private float fireRate;
    [SerializeField] private float shootTimer;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
   // [SerializeField] private Transform target;
    private bool hasShot;
    public bool left;


    public override void Update()
    {
        base.Update();
        if (hasShot)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer > fireRate)
            {
                shootTimer = 0;
                hasShot = false;

            }
        }
    }

    public override void Chase()
    {
        base.Chase();
        EnemyShoot();
    }

    public void EnemyShoot()
    {
        if (!hasShot)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
            bulletSpawnPoint.transform.position = new Vector3(bulletSpawnPoint.position.x, target.transform.position.y, bulletSpawnPoint.position.z);
            hasShot = true;
        }
    }
}
