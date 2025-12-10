using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool GameIsPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
            SceneManager.LoadScene("LevelOne");

        if (Input.GetKeyDown(KeyCode.Alpha8))
            SceneManager.LoadScene("LevelTwo");

        if (Input.GetKeyDown(KeyCode.Alpha9))
            SceneManager.LoadScene("LevelThree");
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // esto congela el juego
        GameIsPaused = true;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // asegurarse de volver el tiempo a la normalidad
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Resume();
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
