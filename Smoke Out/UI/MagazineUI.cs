using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MagazineUI : MonoBehaviour
{
    public TextMeshPro MagazineText;
    public TextMeshPro GunText;
    private int _ammoText;

    /// <summary>
    /// Updates the Magazine amount on both the magazine and on the gun.
    /// </summary>
    /// <param name="_currentAmmo"></param>
    public void UpdateMagazineText(int _currentAmmo)
    {
        MagazineText.text = _currentAmmo.ToString();
        GunText.text = _currentAmmo.ToString();
    }

    public void ReloadMagazineText(int _ammo)
    {
        _ammoText = _ammo;
        Invoke("UpdateText", 1f);
    }
    public void UpdateText()
    {
        MagazineText.text = _ammoText.ToString();
        GunText.text = _ammoText.ToString();
    }
}
