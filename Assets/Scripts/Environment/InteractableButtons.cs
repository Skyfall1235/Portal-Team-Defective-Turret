using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButtons : MonoBehaviour
{
    [SerializeField] GameObject door;


    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.layer == 6)
        {
            door.SetActive(false);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
 
        if (collision.gameObject.layer == 6)
        {
            door.SetActive(true);
        }
    }
}
