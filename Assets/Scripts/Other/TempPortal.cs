using UnityEngine;

/* Assignment: Portal
/  Programmer: Alden Chappell
/  Class Section: SGD.285.4171
/  Instructor: Locklear
/  Date: 3/29/2024
*/

public class TempPortal : MonoBehaviour
{
    [SerializeField] private TempPortal targetPortal;
    public Transform targetPosition;
    
    private void Teleport(GameObject player)
    {
        if(targetPortal != null && targetPortal.targetPosition != null)
        {
            player.transform.position = targetPortal.targetPosition.position;
            player.transform.rotation = targetPortal.targetPosition.rotation;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        Teleport(other.gameObject);
    }
}
