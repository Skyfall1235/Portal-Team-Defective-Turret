using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

class PlayerUIManager : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject winScreen;
    public void OnRestartButtonPress()
    {
        print("Pressed");
        string sceneIndex = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneIndex);
    }

    public void OnQuitButtonClick()
    {
        print("Pressed Quit");
        Application.Quit();
    }
}
