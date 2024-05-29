using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum ELevels
{
    LoadingScreen,
    MainScreen,
    Tutorial,
    Level1,
    EndScreen,
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int currentCubeScore;
    public int totalCubeScore;
    public int deathCount;
    public Transform playerPrefabPos;
    public Transform playerPos;
    public List<TextMeshProUGUI> UIText;
    public bool canMoveCam = false;
    private PlayerMovement _playerMovement;
    private PlayerLife _playerLife;
    private ClassManager _classManager;
    
    

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        LoadSceneByName();
        SceneManager.sceneLoaded += OnSceneLoaded;
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _playerLife = FindObjectOfType<PlayerLife>();
        _classManager = FindObjectOfType<ClassManager>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            RestartLevel();
        }
    }
    public void LoadSceneByName(ELevels level = ELevels.MainScreen)
    {
        SceneManager.LoadSceneAsync((int)level);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Because of the DontDestroyOnLoad, Every new scene you need to a new reference.
        _playerMovement = FindObjectOfType<PlayerMovement>();
        playerPrefabPos = GameObject.FindWithTag("Player+Camera").transform;
        _playerLife = FindObjectOfType<PlayerLife>();
        playerPos = GameObject.FindWithTag("Player").transform;
        _classManager = FindObjectOfType<ClassManager>();

        // Puts the player in specific place when swapping scene.
        switch (scene.buildIndex)
        {
            case 0:
            break;
            case 1:
                playerPrefabPos.position = new Vector3(0, 50, 0); //MainScreen
                playerPos.position = new Vector3(0, 50, 0); //MainScreen
                canMoveCam = false;
                Physics.SyncTransforms();
                break;
            case 2:
                playerPrefabPos.position = new Vector3(5, 7, 0); //Tutorial Level
                playerPos.position = new Vector3(5, 7, 0); //Tutorial Level
                canMoveCam = true;
                Physics.SyncTransforms();
                _playerMovement.rb.useGravity = true;
                break;
            case 3:
                playerPrefabPos.position = new Vector3(-6.3f, 5, 5); //Level 1
                playerPos.position = new Vector3(-6.3f, 5, 5); //Level 1
                Physics.SyncTransforms();
                break;
            case 4:
                playerPrefabPos.position = new Vector3(0, 50, 0); //EndScreen
                playerPos.position = new Vector3(0, 50, 0); //EndScreen
                canMoveCam = false;
                Physics.SyncTransforms();
                break;
        }
    }
    /// <summary>
    /// Holds the total cube Score. 
    /// </summary>
    public void CubeScore() 
    {
        totalCubeScore = currentCubeScore;
    }

    public void ResetCubeScore() 
    {
        currentCubeScore = totalCubeScore;
    }

    GameManager()
    {
        Instance = this;
    }

    public void Die()
    {
        ResetCubeScore();
        deathCount++;
        UIText[0].text = "Deaths: " + deathCount;
        _playerLife.RestartLevel();
    }

    private void RestartLevel() 
    {
        // Resets all the value's to its default.
        GameManager.Instance.LoadSceneByName(ELevels.MainScreen);
        totalCubeScore = 0;
        currentCubeScore = 0;
        deathCount = 0;
        _classManager.styleChosen = EStyleType.NONE;
    }
}
