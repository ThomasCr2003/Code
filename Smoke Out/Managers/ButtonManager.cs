using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    /// <summary>
    /// Loads Scene Which has been given.
    /// </summary>
    /// <param name="scene"></param>
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    /// <summary>
    /// So it Resumes Game when loading other scene.
    /// </summary>
    /// <param name="scene"></param>
    public void LoadSceneFromPauseMenu(string scene)
    {
        SceneManager.LoadScene(scene);
        PauseManager.Instance.ResumeGame();
    }
    public void Quit()
    {
        Application.Quit();
    }

}
