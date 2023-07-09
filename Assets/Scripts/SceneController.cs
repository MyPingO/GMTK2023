using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void ToMainScreenLevel()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public static void ToGameLevel()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public static void RestartGameLevel()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadSceneAsync(1);
    }
}
