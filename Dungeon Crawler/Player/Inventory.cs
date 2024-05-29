using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public ScriptableWeapons[] inventory = new ScriptableWeapons[2];
    public ScriptableWeapons activeWeapon;
    private int activeWeaponIndex;

    public Inventory()
    {
        AddWeapon(GameManager.instance.GetScriptableObject(0));
        AddWeapon(GameManager.instance.GetScriptableObject(1));
        SetWeapon(FromInventoryByIndex(0), 0);
    }

    public void DebugInventory()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null)
            {
                Debug.Log("Inventory Contents:  " + inventory[i].name);
            }
            else
            {
                Debug.Log("Found null value in inventory array");
            }
        }
        Debug.Log("Inventory total length:   " + inventory.Length);
    }

    public void SwapWeapon()
    {
        if (inventory.Length < 1)
        {
            return;
        }
        if (activeWeaponIndex == 0)
        {
            SetWeapon(FromInventoryByIndex(1), 1);
        }
        else
        {
            SetWeapon(FromInventoryByIndex(0), 0);
        }
        
    }

    /// <summary>
    /// Checks if there is already a weapon in the inventory. If so just continue of not add the weapon to that slot.
    /// </summary>
    /// <param name="weapon"></param>
    public void AddWeapon(ScriptableWeapons weapon)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null)
            {
                DebugInventory();
                continue;
            }
            else
            {
                inventory[i] = weapon;
                DebugInventory();
                return;
            }
        }
    }

    public void SetWeapon(ScriptableWeapons weapon, int index)
    {
        activeWeapon = weapon;
        activeWeaponIndex = index;
        Player.instance.DeSpawnWeapon();
        Player.instance.SpawnWeapon(activeWeapon);
    }

    public void RemoveWeapon()
    {

    }

    public ScriptableWeapons FromInventoryByIndex(int index)
    {
        return inventory[index];
    }

    public ScriptableWeapons GetActiveWeapon()
    {
        return activeWeapon;
    }
}
