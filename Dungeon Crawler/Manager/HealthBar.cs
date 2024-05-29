using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthbar;
    private float maxHealth = 3f;

    private void Start()
    {
        healthbar = GetComponent<Image>();
    }

    private void Update()
    {
        healthbar.fillAmount = Player.instance.playerHealth / maxHealth;
    }
}
