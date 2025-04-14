using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interact : MonoBehaviour
{
    public float InteractRange;
    public GameObject InteractIndicator;

    void FixedUpdate()
    {
        if (Camera.main != null)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, InteractRange))
            {
                if (hit.collider.GetComponent<Interactable>() != null)
                {
                    InteractIndicator.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hit.collider.GetComponent<Interactable>().Interact();
                    }
                    return;
                }
                else
                {
                    InteractIndicator.SetActive(false); // Disable indicator when no valid item is detected
                }
            }
            else
            {
                InteractIndicator.SetActive(false);
            }
        }

    }

    private void OnDrawGizmos()
    {
        // Visualize the raycast in the Scene view
        if (Camera.main != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * InteractRange);
        }
    }
}
