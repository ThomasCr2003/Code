using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Abilities
{
    Fireball,
    SpawnAllyDog,
}

public class Player : MonoBehaviour , IDamagable
{
    public static Player instance { get; private set; }
    public int playerHealth = 3;
    private Animator animator;
    #region PlayerMovement Variables
    public float movementSpeed = 100;
    public Vector2 velocity;
    private Rigidbody2D rb2d;
    public bool canMove;
    #endregion
    #region Abilities and Inventory Information
    public ScriptableWeapons scriptableObject;
    public Inventory inv;
    public GameObject arrowprefab;
    public Transform shooting;
    private GameObject weaponObject;
    private Vector2 lastLookDirection;
    private float fireballCooldown;
    [SerializeField] private float fireballCD;
    [SerializeField] private GameObject fireballPrefab;
    private Abilities abilities;
    #endregion

    Player()
    {
        instance = this;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        rb2d = GetComponent<Rigidbody2D>();
        inv = new Inventory();
        animator = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        if (canMove)
        {
            Vector2 inputVector = Input.GetAxisRaw("Vertical") != 0 ? new Vector2(0, Input.GetAxisRaw("Vertical")) : new Vector2(Input.GetAxisRaw("Horizontal"), 0);
            velocity = inputVector;
        }

        if (velocity != Vector2.zero)
            lastLookDirection = velocity;

        AttackWithWeapon();
        Abilities();
        //SwapsWeapon
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inv.SwapWeapon();
        }
        Debug.Log(fireballCooldown);
        if (playerHealth <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        rb2d.velocity = velocity * movementSpeed * Time.deltaTime;
    }

    /// <summary>
    /// If velocity == 0,0 return lastlookdirection if false return velocity.
    /// </summary>
    /// <returns></returns>
    public Vector2 DirectionCalculation()
    {
        return velocity == Vector2.zero ? lastLookDirection : velocity;
    }

    public void DeSpawnWeapon()
    {
        if (weaponObject != null)
        {
            Destroy(weaponObject);
        }
        else
        {
            return;
        }
    }


    public void SpawnWeapon(ScriptableWeapons weapon)
    {
        weaponObject = Instantiate(weapon.prefab, gameObject.transform.GetChild(0));
    }

    public void AttackWithWeapon()
    {
        switch (inv.GetActiveWeapon().weaponType)
        {
            case WeaponType.Sword:
            case WeaponType.Axe:
            case WeaponType.Mace:
                
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    animator = GetComponentInChildren<Animator>();
                    animator.SetBool("Attack",true);
                    Invoke("ResetAttackAnimation", 0.1f);
                }
                break;
            case WeaponType.Bow:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    ShootingArrow(inv.GetActiveWeapon().weaponDamage);
                }
                break;
            default:
                break;
        }
    }

    public void ShootingArrow(int damage)
    {
        GameObject arrow = Instantiate(arrowprefab, shooting);
        arrow.GetComponent<Projectile>().damage = damage;
        arrow.transform.SetParent(null);
    }

    public void ShootFireball(int damage)
    {
        GameObject fireball = Instantiate(fireballPrefab, shooting);
        fireball.GetComponent<Projectile>().damage = damage;
        fireballCooldown = fireballCD;
        fireball.transform.SetParent(null);
    }

    public void Abilities()
    {
        switch (abilities)
        {
            case global::Abilities.Fireball:
                if (fireballCooldown > 0)
                {
                    fireballCooldown--;
                }
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    if (fireballCooldown <= 0)
                    {
                        ShootFireball(1);
                    }
                    
                }
                break;
            case global::Abilities.SpawnAllyDog:
                break;
            default:
                break;
        }
    }

    private void Die()
    {
        SceneManager.LoadScene("EndScreen");
        GameManager.instance.playerList.Clear();
        Destroy(gameObject);
    }

    private void ResetAttackAnimation()
    {
        Debug.Log("Reset animation");
        animator.SetBool("Attack", false);
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
    }

    public void AddPlayerHealth(int amount)
    {
        if (playerHealth < 3)
        {
            playerHealth += amount;
        }
        else
        {
            playerHealth = 3;
        }
    }
}
