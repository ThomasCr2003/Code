using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<GameObject> Enemies = new List<GameObject>();

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void CheckForEnemiesLeft(int amountOfEnemies)
    {
        if (amountOfEnemies <= 0)
        {
            GameOver();
        }
    }

    #region GameOverLogic

    public void GameOver()
    {
        PlayerPrefs.Save();
        ButtonManager.Instance.LoadScene("Gameover");
    }

    public void Victory()
    {
        PlayerPrefs.Save();
        ButtonManager.Instance.LoadScene("Main Menu");
    }

    #endregion GameOverLogic

    
}
