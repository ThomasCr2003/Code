using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum Scenes
{
    LoadingScene,
    MainScreen,
    Hub,
    Level1,
    Level2,
    Level3,
    LevelBoss,
    Controls,
    EndScreen,
}

public class SceneSwap : MonoBehaviour
{
    
    public Scenes scene;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(scene.ToString());
            switch (scene)
            {
                case Scenes.LoadingScene:
                    break;
                case Scenes.Hub:
                    break;
                case Scenes.Level1:
                case Scenes.Level2:
                case Scenes.Level3:
                case Scenes.LevelBoss:
                    GameManager.instance.SetSavePosition(collision.gameObject.transform.position);
                    break;
                default:
                    break;
            }
        }
    }
}
