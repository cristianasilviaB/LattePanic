using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnApplicationPause()
    {
     
    }

}
