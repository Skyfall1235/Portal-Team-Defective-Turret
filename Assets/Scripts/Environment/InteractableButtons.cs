using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableButtons : MonoBehaviour
{
    [SerializeField] GameObject door;
    public UnityEvent<bool> OnCollision = new UnityEvent<bool>();


    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.layer == 6)
        {
            OnCollision.Invoke(false);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
 
        if (collision.gameObject.layer == 6)
        {
            OnCollision.Invoke(true);
        }
    }
}
