using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Levels
{
    Loading,
    MainScreen,
    Tutorial,
    Level1,
    Level2,
    GameOver,
    CreditScene,
    EndScreen,
}
public class GameManager : MonoBehaviour
{
    //Array of the managers.
    private static Manager[] managers;
    public static GameManager instance { get; private set; }
    private Timer loadingTimer;
    public List<Enemy> enemies = new List<Enemy>();
    public List<GameObject> doors = new List<GameObject>();
    public bool tutorialCompleted;
    GameManager() 
    {
        instance = this;
        loadingTimer = new Timer();
        managers = new Manager[]
        {
            new TutorialManager(),

        };
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        loadingTimer.SetTimer();

        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Start();
        }
    }

    private void Update()
    {
        if (loadingTimer.isActive && loadingTimer.TimerDone())
        {
            loadingTimer.StopTimer();
            LoadLevel(Levels.MainScreen);
        }

        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Update();
        }
    }

    /// <summary>
    /// Checks if the manager you want to get is really a manager. And makes that you can a manager from anywhere.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetManager<T>() where T : Manager
    {
        for (int i = 0; i < managers.Length; i++)
        {
            if (typeof(T) == managers[i].GetType())
            {
                return (T)managers[i];
            }
        }
        return default(T);
    }

    public static void LoadLevel(Levels level)
    {
        SceneManager.LoadScene((int)level);
    }

    public void AddDoors(int order, GameObject door)
    {
        doors.Insert(order, door);
    }
}
