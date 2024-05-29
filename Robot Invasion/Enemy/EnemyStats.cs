using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType
{
    Base,
    Turret,
    Sniper,
    Suicider,
}
public class EnemyStats : MonoBehaviour
{
    public int enemyHealth;
    public float enemySpeed;
    public int bulletSpeed;
    public int bulletDamage;
    public float fireRate;
    public int enemyAmmo;
    public int enemyAmmoClip;
    public float enemyReloadTimer;
    public EnemyType enemyType;
    public bool needsAnimator;
    public bool enemySuicide;
    public bool enemySniper;
    public bool enemyBase;

    private void Start()
    {
        switch (enemyType)
        {
            case EnemyType.Base:
                enemySpeed = 10;
                enemyHealth = 30;
                bulletSpeed = 10;
                bulletDamage = 15;
                fireRate = 0.75f;
                enemyAmmo = 30;
                enemyAmmoClip = 30;
                enemyReloadTimer = 2;
                needsAnimator = true;
                enemyBase = true;
                break;
            case EnemyType.Turret:
                enemyHealth = 100;
                enemySpeed = 0;
                bulletSpeed = 10;
                bulletDamage = 10;
                fireRate = 0.25f;
                enemyAmmo = 100;
                enemyAmmoClip = 100;
                enemyReloadTimer = 10;
                break;
            case EnemyType.Sniper:
                enemySpeed = 10;
                enemyHealth = 50;
                bulletSpeed = 20;
                bulletDamage = 50;
                fireRate = 2;
                enemyAmmo = 5;
                enemyAmmoClip = 5;
                enemyReloadTimer = 2;
                enemySniper = true;
                break;
            case EnemyType.Suicider:
                enemyHealth = 20;
                enemySpeed = 5;
                bulletDamage = 75;
                enemySuicide = true;
                break;
            default:
                break;
        }
    }
}
