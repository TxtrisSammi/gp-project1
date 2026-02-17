using UnityEngine;
using UnityEngine.SceneManagement;

public class TestGameManager : MonoBehaviour
{
    void Start()
    {
        // Test global access
        Debug.Log("Testing GameManager Singleton...");

        // Start the game
        GameManager.Instance.StartGame();
    }

    void Update()
    {
        // Test pause/resume
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.Instance.PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.ResumeGame();
        }

        // Load next scene to test persistence
        if (Input.GetKeyDown(KeyCode.N))
        {
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextScene < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }
}