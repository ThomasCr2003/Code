using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons", fileName = "Weapon", order = 1)]
public class ScriptableWeapons : ScriptableObject
{
    public string weaponName;
    public int weaponDamage;
    public GameObject prefab;
    public WeaponType weaponType;
}

public enum WeaponType
{
    Sword,
    Axe,
    Mace,
    Bow
}
