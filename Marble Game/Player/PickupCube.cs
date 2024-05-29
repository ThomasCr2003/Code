using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickupCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            GameManager.Instance.currentCubeScore++;
            GameManager.Instance.UIText[1].text = "Cubes: " + GameManager.Instance.currentCubeScore;
            Destroy(other.gameObject);
        }
    }

}
