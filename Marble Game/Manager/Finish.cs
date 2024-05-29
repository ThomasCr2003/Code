using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private bool _completedLevel = false;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_completedLevel)
        {
            Invoke("CompleteLevel", 0.5f);
            _completedLevel = true;
            Debug.Log("LevelCompleted");
            GameManager.Instance.CubeScore();
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
