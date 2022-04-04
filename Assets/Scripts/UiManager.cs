using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance = null;

    public GameObject levelCompleteObj;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else if (Instance == null)
        {
            Instance = this;
        }
    }
    public void Pause(bool isPaused)
    {
        Time.timeScale = isPaused == true ? 0 : 1;
    }
    public void MainMenuBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void LevelComplete()
    {
        levelCompleteObj.SetActive(true);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
