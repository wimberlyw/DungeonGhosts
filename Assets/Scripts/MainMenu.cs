using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
        {
            SceneManager.LoadScene("Play Scene");

        }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PartnerPlayGame()
    {
        SceneManager.LoadScene("Offline");

    }


}

