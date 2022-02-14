using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoseMenu : MonoBehaviour
{
    public void ResetGame ()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void QuitGame ()
    {
        Debug.Log ("Quit");
        Application.Quit();
        //SceneManager.LoadSceneAsync("MainMenu");
    }
}
