using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeathBox"))
        {
            GameManager.Instance.Die();
        }
    }
    public void RestartLevel()
    {
        string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
