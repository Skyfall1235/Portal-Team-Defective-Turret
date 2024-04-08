/* Assignment: Portal
/  Programmer: Owen Jones
/  Class Section: SGD.285.4171
/  Instructor: Locklear
*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUIManager : MonoBehaviour
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
