using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LinkedPortal : MonoBehaviour
{
    private LayerMask m_playerLayerMask;
    private LayerMask m_targetLMs;
    private Transform m_spawnTransform;
    private float m_forwardTeleportForce = 0f;

    private UnityEvent<GameObject, LinkedPortal> m_onCollisionWithPortal = new UnityEvent<GameObject, LinkedPortal>();

    public void Teleport(GameObject GO)
    {
        //set the objects position to that of the spawn transform
        GO.transform.position = m_spawnTransform.position;
        PushNonPlayerObjects(GO);
    }

    private void PushNonPlayerObjects(GameObject GO)
    {
        //only apply forces to the non player mask
        if(GO.layer != m_playerLayerMask)
        {
            Rigidbody rb = GO.GetComponent<Rigidbody>();
            if(rb != null )
            {
                rb.AddRelativeForce(FindVectorOfForce(), ForceMode.Impulse);
            }
        }
    }
    private Vector3 FindVectorOfForce()
    {
        //transform.position to spawn transform direction
        Vector3 forwardVector = m_spawnTransform.position - transform.position;
        //normalize and multiply by force 
        forwardVector.Normalize();
        forwardVector = forwardVector * m_forwardTeleportForce;
        return forwardVector;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //
    }
}
