using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddToGameManager : MonoBehaviour
{
    public int TextList;
    private void Start()
    {
        GameManager.Instance.UIText.Insert(TextList,gameObject.GetComponent<TextMeshProUGUI>());
        if (TextList == 0)
        {
            GameManager.Instance.UIText[0].text = ("Deaths : " + GameManager.Instance.deathCount);
        }
        if (TextList == 1)
        {
            GameManager.Instance.UIText[1].text = "Cubes : " + GameManager.Instance.currentCubeScore;
        }
    }
}
