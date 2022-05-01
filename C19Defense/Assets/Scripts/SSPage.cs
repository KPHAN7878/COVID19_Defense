using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SSPage : MonoBehaviour
{
    //Transitions from the Start screen to the login page
    public void SStoLogin()
    {
        SceneManager.LoadScene(1);
    }
}
