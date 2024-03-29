using UnityEngine;
/* Assignment: Portal
/  Programmer: Alden Chappell
/  Class Section: SGD.285.4171
/  Instructor: Locklear
/  Date: 02/29/2024
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
            RespawnPickupNearPlayer(other.gameObject);
        }

        
        
        Vector3 originalPlayerPosition = other.gameObject.transform.position;
        float playerAfterRaising = other.gameObject.transform.position.y;
        if (!isPickedUp && playerAfterRaising > originalPlayerPosition.y)
        {
            RespawnPickupNearPlayer(other.gameObject);
        }
    }

    //Respawn the pickup in front of the player
    public void RespawnPickupNearPlayer(GameObject player)
    {
        //Force the player to drop this pickup
        PickupItems pickupItems = player.GetComponent<PickupItems>();
        pickupItems.DropPickup();
    
        //Move the pickup above the player's head
        Vector3 playerPosition = player.transform.position;
        float halfPlayerHeight = player.GetComponent<CharacterController>().height / 2.0f; // Get half the height of the player's collider
        float halfPickupHeight = GetComponent<Collider>().bounds.extents.y / 2.0f; // Get half the height of the pickup's collider
        
        
        //If it is found that the pickup is going out of the map from in front of the player,
        //Remove the + playerHeight + pickupHeight from the third vector of pickupSpawnPosition.
        Vector3 pickupSpawnPosition = new Vector3(
            playerPosition.x,
            playerPosition.y + halfPlayerHeight + halfPickupHeight, //Above the players head by half of the player's size and this objects size.
            playerPosition.z + halfPlayerHeight + halfPickupHeight); //In front of the player by half of the player's size and this objects size.
        
        transform.position = pickupSpawnPosition;
    }

}
