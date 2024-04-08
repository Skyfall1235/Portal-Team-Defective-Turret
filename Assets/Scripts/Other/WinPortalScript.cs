/* Assignment: Portal
/  Programmer: Owen Jones
/  Class Section: SGD.285.4171
/  Instructor: Locklear
/  Date: 03/29/2024
*/
using UnityEngine;

public class WinPortalScript : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        print("Collided");
        if (other.gameObject.tag == "Player")
        {
            other.transform.GetComponentInChildren<PlayerUIManager>().winScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            other.transform.GetComponent<CharacterController>().enabled = false;
        }
    }
}
