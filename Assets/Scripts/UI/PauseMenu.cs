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
        {
            Resume();
            SceneManager.LoadScene("LevelOne");
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Resume();
            SceneManager.LoadScene("LevelTwo");
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Resume();
            SceneManager.LoadScene("LevelThree");
        }     
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        AudioSource[] allAudio = GameObject.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        foreach (AudioSource a in allAudio)
        {
            a.UnPause();
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        AudioSource[] allAudio = GameObject.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        foreach (AudioSource a in allAudio)
        {
            a.Pause();
        }


    }

    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Resume();
    }

    public void LoadMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
