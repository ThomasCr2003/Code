using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void OnButtonClick() 
    {
        GameManager.Instance.LoadSceneByName(ELevels.Tutorial);
    }
}
