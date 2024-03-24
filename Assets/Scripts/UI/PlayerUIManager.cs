using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

class PlayerUIManager : MonoBehaviour
{
    public GameObject deathScreen;
    public void OnRestartButtonPress()
    {
        string sceneIndex = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneIndex);
    }
}
