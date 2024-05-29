using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    [SerializeField] private float fireRate;
    [SerializeField] private float shootTimer;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] bulletSpawnPoint;
    private bool hasShot;

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
        EnemyShoot();
    }


    public void EnemyShoot()
    {
        if (!hasShot)
        {
            for (int i = 0; i < bulletSpawnPoint.Length; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint[i].transform.position, bulletSpawnPoint[i].transform.rotation);
                bulletSpawnPoint[i].transform.position = new Vector3(bulletSpawnPoint[i].position.x, bulletSpawnPoint[i].position.y, bulletSpawnPoint[i].position.z);
                hasShot = true;
            }
            
        }
    }
}
