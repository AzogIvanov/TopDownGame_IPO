using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SceneManager.LoadScene("LevelOne");

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SceneManager.LoadScene("LevelTwo");

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SceneManager.LoadScene("NivelThree");
    }

    public void PlayGame()
    {
        // Cargar primer nivel
        SceneManager.LoadScene("LevelTwo");
    }

    public void OpenOptions()
    {
        // Activar panel opciones
        // Panel.SetActive(true);
        Debug.Log("Opciones abiertas (aún sin implementar)");
    }

    public void QuitGame()
    {
        Debug.Log("Salir del juego");
        Application.Quit();

        // IMPORTANTE: Esto solo se ve en build,
        // en el editor no se cerrará el juego.
    }
}
