using UnityEditor.PackageManager;
using UnityEngine;
/* Assignment: Portal
/  Programmer: Alden Chappell
/  Class Section: SGD.285.4171
/  Instructor: Locklear
/  Date: 29/29/2024
*/

[RequireComponent(typeof(Rigidbody))]
public class Pickup : MonoBehaviour
{
    public bool isPickedUp = false;

    //How far this pickup should move when colliding with the player
    private const float XMovementOffset = .05f;
    
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        if (isPickedUp)
        {
            PickupItems pickupItems = other.gameObject.GetComponent<PickupItems>();
            pickupItems.DropPickup();
                
            MovePickupAbovePlayer(other.gameObject);
        }
    }

    //Move the pickup above 
    private void MovePickupAbovePlayer(GameObject player)
    {
        var playerPosition = player.transform.position;
        
        transform.position = new Vector3(
            playerPosition.x - XMovementOffset,
            playerPosition.x,
            playerPosition.z);
    }
}
