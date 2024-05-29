using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void OnButtonClickStart()
    {
        GameManager.instance.LoadHub();
    }

    public void OnButtonClickControls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void OnButtonClickMainScreen()
    {
        SceneManager.LoadScene("MainScreen");
    }

    public void OnButtonClickQuit()
    {
        Application.Quit();
    }
}
