using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    private GameManager _instance;
    private ClassManager _classManager;
    private void Start()
    {
        _instance = GameManager.Instance;
        _classManager = FindObjectOfType<ClassManager>();
    }

    public void OnButtonClick()
    {
        // Resets all the value's to its default.
        GameManager.Instance.LoadSceneByName(ELevels.MainScreen);
        _instance.totalCubeScore = 0;
        _instance.currentCubeScore = 0;
        _instance.deathCount = 0;
        _classManager.styleChosen = EStyleType.NONE;
    }
}
