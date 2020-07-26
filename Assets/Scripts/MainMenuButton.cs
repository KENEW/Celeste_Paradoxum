using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void OnStartButton()
    {
        SceneManager.LoadScene("VillageScene");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}