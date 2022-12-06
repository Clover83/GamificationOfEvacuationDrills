using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    //public GameObject PauseMenu;
    //public static bool gameIsPaused;

    public GameObject ArSession;
    void Start()
    {

        Time.timeScale = 0f;


    }
   public void PauseGame()
   {
        Time.timeScale = 0f;
        ArSession.SetActive(false);
    }
   public void UnPauseGame()
   {
       Time.timeScale = 1;
       ArSession.SetActive(true);
   }
   //public void QuitGame()
   //{
   //    Application.Quit();
   //    //Debug.Log("Game is exiting");
   //}
}
