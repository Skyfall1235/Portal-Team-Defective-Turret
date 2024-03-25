using UnityEngine;
using UnityEngine.SceneManagement;

/* Assignment: Portal
/  Programmer: Alden Chappell
/  Class Section: SGD.285.4171
/  Instructor: Locklear
/  Date: 03/25/2024
*/
public class MainMenuButtonController : MonoBehaviour
{
    [SerializeField] private GameObject buttonPanel;
    [SerializeField] private GameObject instructionsPanel;
    
    /// <summary>
    /// Upon calling this method, the scene specified in the parameter will be loaded.
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);

    /// <summary>
    /// Upon calling this method, the application will close.
    /// </summary>
    public void QuitGame() => Application.Quit();
    
    /// <summary>
    /// Upon calling this method, the instructions panel will set to active, and the button panel will be deactivated.
    /// </summary>
    /// <param name="instructionsLoaded"></param>
    public void LoadInstructions(bool instructionsLoaded)
    {
        buttonPanel.SetActive(!instructionsLoaded); //disable main button panel
        instructionsPanel.SetActive(instructionsLoaded); //enable instructions panel
    }
}
