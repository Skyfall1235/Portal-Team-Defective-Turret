using System.Collections;
using System.Collections.Generic;
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
