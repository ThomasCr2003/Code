using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{
    public TextMeshProUGUI TextPlacement;
    public string[] MovementText;
    public string[] MechanicText;
    public int Index;
    private bool _textHasBeenDone;

    public void StartTutorialText()
    {
        if (GameManager.Instance != null)
        {
            if (!GameManager.Instance.TutorialHasBeenShown)
            {
                NextMovementTutorialText();
                GameManager.Instance.TutorialHasBeenShown = true;
                _textHasBeenDone = true;
            }
            else
            {
                return;
            }
        }
    }

    public void StartMechanicTutorialText()
    {
        if (GameManager.Instance != null)
        {
            if (_textHasBeenDone)
            {
                NextMechanicTutorialText();
            }
        }
    }

    private void NextMovementTutorialText()
    {
        if (Index < MovementText.Length)
        {
            StartCoroutine("NextMovementText");
        }
        else
        {
            Index = 0;
            return;
        }
    }

    private void NextMechanicTutorialText()
    {
        Debug.Log("Index = " + Index);
        if (Index < MechanicText.Length)
        {
            Debug.Log("Start Text");
            StartCoroutine("NextMechanicText");
        }
        else
        {
            Index = 0;
            return;
        }
    }

    private int GetCurrentIndex() 
    { 
        return Index; 
    }

    private IEnumerator NextMovementText()
    {
        TextPlacement.text = MovementText[GetCurrentIndex()];
        Index++;
        yield return new WaitForSeconds(5);
        NextMovementTutorialText();
        yield return null;
    }

    private IEnumerator NextMechanicText()
    {
        Debug.Log("Mechanic Text");
        TextPlacement.text = MechanicText[GetCurrentIndex()];
        Index++;
        yield return new WaitForSeconds(5);
        NextMechanicTutorialText();
        yield return null;
    }
}
