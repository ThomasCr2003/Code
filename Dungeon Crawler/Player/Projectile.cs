using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int projectileSpeed;
    public int damage;
    public Vector2 direction;


    private void Start()
    {
        Destroy(gameObject, 5f);
        direction = Player.instance.DirectionCalculation();
        
    }

    private void Update()
    {
        gameObject.transform.Translate(direction * projectileSpeed * Time.deltaTime);
    }

    public void FireBallDamage(int Fireballdamage)
    {
        Fireballdamage = damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamagable isDamagable = collision.gameObject.GetComponent<IDamagable>();

        if (isDamagable != null)
        {
            isDamagable.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (isDamagable == null)
        {
            Destroy(gameObject);
        }
    }
}
