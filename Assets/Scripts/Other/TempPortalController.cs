using System.Collections;
using UnityEngine;
/* Assignment: Portal
/  Programmer: Alden Chappell
/  Class Section: SGD.285.4171
/  Instructor: Locklear
/  Date: 03/30/2024
*/


public class TempPortalController : MonoBehaviour
{
    public Transform spawnLocation; // The spawn location for the player after teleportation
    public TempPortalController connectedPortal; // The portal that this portal is connected to
    private const float TeleportTime = .1f; // Time taken for teleportation
    private const float TeleportCooldown = 3.0f; // Cooldown time between teleportations
    private bool _isTeleporting; // Flag to prevent multiple teleportations
    private bool _isOnCooldown; // Flag to prevent teleportation during cooldown
    private Coroutine _cooldownCoroutine; // Coroutine for cooldown

    private PickupItems _playerPickups;
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return; //if the collider isnt the player, return 

        _playerPickups = other.GetComponent<PickupItems>();
        
        if (!_isTeleporting && !_isOnCooldown && spawnLocation != null)
        {
            StartCoroutine(TeleportPlayer(other.transform));
        }
    }

    private IEnumerator TeleportPlayer(Transform player)
    {
        _isTeleporting = true;
    
        PlayerMovement playerController = player.gameObject.GetComponent<PlayerMovement>();
        playerController.controller.enabled = false;

        // Start teleportation 
        Transform targetPosition = connectedPortal.spawnLocation;
    
        // Move player to the spawn location of the connected portal
        player.position = targetPosition.position;
        // Change the rotation of the player to the targetPosition's rotation
        player.rotation = targetPosition.rotation;

        playerController.isTeleporting = true;

        yield return new WaitForSeconds(TeleportTime);

        playerController.controller.enabled = true; // Re-enable the CharacterController after teleportation
        playerController.isTeleporting = false;

        // Start cooldown on the connected portal
        connectedPortal._isOnCooldown = true;
        _isOnCooldown = true;
        _cooldownCoroutine = StartCoroutine(StartTeleportCooldown());

        //Respawn the pickup upon teleporting so the player doesn't lose it.
        if (_playerPickups.currentPickup != null) 
        {
            _playerPickups.currentPickup.RespawnPickupInFrontOfPlayer(player.gameObject);
        }
        

        // Reset teleporting flag
        _isTeleporting = false;
    }

    private IEnumerator StartTeleportCooldown()
    {
        yield return new WaitForSeconds(TeleportCooldown);

        _isOnCooldown = false;
        connectedPortal._isOnCooldown = false;
    }


    //-------------------------------------------------------------------------------------
    //Re enable this to see a line from this portal to the connected portal's spawnlocation
    //-------------------------------------------------------------------------------------
    private void OnDrawGizmosSelected()
    {
        if (connectedPortal != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, connectedPortal.spawnLocation.transform.position);
        }
    }
}
