using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    //public GameObject PauseMenu;
    //public static bool gameIsPaused;
    void Start()
    {

        Time.timeScale = 0f;


    }
   public void PauseGame()
   {
        Time.timeScale = 0f;
    }
   public void UnPauseGame()
   {
       Time.timeScale = 1;
   }
   //public void QuitGame()
   //{
   //    Application.Quit();
   //    //Debug.Log("Game is exiting");
   //}
}
