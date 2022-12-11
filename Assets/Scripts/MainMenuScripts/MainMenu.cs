using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // A better name for this function could be "LoadNextScene"
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ReloadScene()
    {
        Caching.ClearCache();
        //SceneManager.UnloadSceneAsync(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Reloads the scene
        Debug.Log("Retry");
    }
    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
