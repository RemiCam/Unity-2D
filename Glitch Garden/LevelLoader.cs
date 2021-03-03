using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    int currentSceneIndex;
    [SerializeField] float timeToLoad = 3f;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex == 0)
        {
            LoadStart();
        }
        
    }

    public void LoadStart()
    {
        StartCoroutine(LoadStartSceneAfterDelay(timeToLoad));
    }

    IEnumerator LoadStartSceneAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start Scene");
    }

    public void LoadOptionMenu()
    {
        SceneManager.LoadScene("Option Screen");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadSameLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadGameOver()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
