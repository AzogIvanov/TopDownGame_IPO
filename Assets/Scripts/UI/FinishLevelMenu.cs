using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevelMenu : MonoBehaviour
{
    public GameObject winMenuUI;
    public GameObject loseMenuUI;
    public GameObject pauseMenu;
    public GameObject gameFinishedMenuUI;
    public static bool GameIsPaused = false;

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Win()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "LevelThree")
            gameFinishedMenuUI.SetActive(true);
        else
            winMenuUI.SetActive(true);

        Time.timeScale = 0f;
        pauseMenu.SetActive(false);
        GameIsPaused = true;
    }
    public void Lose()
    {
        loseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        GameIsPaused = true;
    }


    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Resume();
    }

    public void ContinueLevel()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "LevelOne")
            SceneManager.LoadScene("LevelTwo");

        if (currentScene.name == "LevelTwo")
            SceneManager.LoadScene("LevelThree");

        Resume();
    }

    public void LoadMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");

    }

}

