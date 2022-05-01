using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Loads up the next scene in the build index (Starts the game)
    public void PlayMapEasy()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayMapHard()
    {
        SceneManager.LoadScene(5);
    }

    public void PlayMapFreeplay()
    {
        SceneManager.LoadScene(6);
    }

    //Quits out of the application 
    public void QuitGame()
    {
        Debug.Log("You Have Exited out the game");
        Application.Quit();
    }

}
