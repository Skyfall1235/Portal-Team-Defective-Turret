/* Assignment: Portal
/  Programmer: Wyatt Murray
/  Class Section: SGD.285.4171
/  Instructor: Locklear
/  Date: 3/17/2024
*/
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class LinkedPortal : MonoBehaviour
{
    [SerializeField] private LayerMask m_playerLayerMask;
    [SerializeField] private LayerMask m_pickupLayerMask;
    [SerializeField] private Transform m_spawnTransform;
    [SerializeField] private float m_forwardTeleportForce = 0f;

    public UnityEvent<GameObject, LinkedPortal> m_onCollisionWithPortal = new UnityEvent<GameObject, LinkedPortal>();
    public UnityEvent onTeleportIsTarget = new();

    /// <summary>
    /// teleports a given Gameobject to this portals spawn transform.
    /// If it is not a player object, it gets a force applied to it.
    /// </summary>
    /// <param name="GO">is the GameObject to be teleported</param>
    public void Teleport(GameObject GO)
    {
        //set the objects position to that of the spawn transform
        GO.transform.position = m_spawnTransform.position;
        PushNonPlayerObjects(GO);
        onTeleportIsTarget.Invoke();
    }

    private void PushNonPlayerObjects(GameObject GO)
    {
        //only apply forces to the non player mask
        if(GO.layer != m_playerLayerMask.value)
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
        //check if the object in in the target laymermasks, if it is, go ahead and call the unity event
        int pickupLayer = m_pickupLayerMask.value;
        int playerLayer = m_playerLayerMask.value;
        if(collision.gameObject.layer != pickupLayer || collision.gameObject.layer != playerLayer)
        {
            return;
        }
        m_onCollisionWithPortal.Invoke(collision.gameObject, this);//as in this linked portal
    }
}
