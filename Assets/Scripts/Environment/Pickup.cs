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

    public bool isStoredPickup = false;

    private PickupItems _playerPickupItems;
    
    private void Start()
    {
        _playerPickupItems = GameObject.FindGameObjectWithTag("Player").GetComponent<PickupItems>();

    }


    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        if (isPickedUp)
        {
            RespawnPickupInFrontOfPlayer(other.gameObject);
        }
        
        Vector3 originalPlayerPosition = other.gameObject.transform.position;
        float playerAfterRaising = other.gameObject.transform.position.y;
        if (!isPickedUp && playerAfterRaising > originalPlayerPosition.y)
        {
            RespawnPickupInFrontOfPlayer(other.gameObject);
        }
    }

    //Respawn the pickup in front of the player
    public void RespawnPickupInFrontOfPlayer(GameObject player)
    {
        // Force the player to drop this pickup
        PickupItems pickupItems = player.GetComponent<PickupItems>();
        pickupItems.DropPickup();
    
        // Calculate the respawn position for the pickup near the player
        Vector3 playerPosition = player.transform.position;
        Vector3 respawnPosition = playerPosition + player.transform.forward * 2.0f; // 2 units in front of the player.
    
        // Move the pickup to the respawn position
        transform.position = respawnPosition;
    }

}
