using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _PauseScreen;

    public bool IsPaused;

    public static PauseManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "Level 1")
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        ValueSaveGet();
        if (IsPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    /// <summary>
    /// Pauze's Game.
    /// </summary>
    private void PauseGame()
    {
        IsPaused = true;
        _PauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    /// <summary>
    /// Resume Game.
    /// </summary>
    public void ResumeGame()
    {
        IsPaused = false;
        _PauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    /// <summary>
    /// Gets And Sets The Value's
    /// </summary>
    private void ValueSaveGet()
    {
        ControlsManager.Instance.GetSavedSettings();
        ControlsManager.Instance.SetValue();
    }
}
