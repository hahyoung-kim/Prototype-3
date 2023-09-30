using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
    #if !UNITY_WEBGL
        Application.Quit();
    #else
        Application.OpenURL("about:blank");
    #endif
    }
}