using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Thomas");
    }

    public void CreditScene()
    {
        SceneManager.LoadScene("Credit");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    
    public void Play()
    {
        SceneManager.LoadScene("Dennis");
    }
}
