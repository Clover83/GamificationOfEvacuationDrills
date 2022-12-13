using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script to move between scenes or reload them
public class MainMenu : MonoBehaviour
{
    //A better name for this function could be "LoadNextScene"
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Load a scene using index
    public void Load(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void ReloadScene()
    {
        Caching.ClearCache();
        //Reloads the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Retry");
    }

    public void BackToMain()
    {
        //Implies that the scene name should always be MainMenu or index 0
        //SceneManager.LoadScene(0);
        SceneManager.LoadScene("MainMenu");
    }
}
