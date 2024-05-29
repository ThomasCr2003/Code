using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private float timer;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timer++;
            if (timer >= 2)
            {
                timer = 0;
                GameManager.LoadLevel(Levels.Level2);
            }
        }
    }
}
