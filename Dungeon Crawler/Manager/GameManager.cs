using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public Player player;
    public List<Player> playerList = new();
    [SerializeField] private Vector2 savedPlayerPos;

    public ScriptableWeapons[] loadedObjects;

    GameManager()
    {
        instance = this;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// Checks when scene swapping if player is loaded in.
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if ("Hub".Equals(scene.name))
        {
            if (playerList.Count < 1)
            {
                Instantiate(player);
                playerList.Add(player);
            }
            else
            {
                Player.instance.transform.position = GetSavePosition();
            }
        }
        if ("LoadingScene".Equals(scene.name))
        {
            Invoke("LoadMainScreen",0.1f);
        }
    }

    public void LoadHub()
    {
        SceneManager.LoadScene("Hub");
    }

    public void LoadMainScreen()
    {
        SceneManager.LoadScene("MainScreen");
    }
    /// <summary>
    /// Gets the player position when scene swapping away from the hub.
    /// </summary>
    /// <returns></returns>
    public Vector2 GetSavePosition()
    {
        return savedPlayerPos;
    }

    /// <summary>
    /// Gets the player position when scene swapping away from the hub.
    /// </summary>
    /// <param name="position"></param>
    public void SetSavePosition(Vector2 position)
    {
        savedPlayerPos = new Vector2(position.x - 0.1f,position.y );
    }
    public ScriptableWeapons GetScriptableObject(int index)
    {
        return loadedObjects[index];
    }
}
