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
    
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) return; //if the collider isnt the player, return 
        
        if (!_isTeleporting && !_isOnCooldown && spawnLocation != null)
        {
            StartCoroutine(TeleportPlayer(other.transform));
        }
    }

    private IEnumerator TeleportPlayer(Transform player)
    {
        _isTeleporting = true;
        
        // Start teleportation effect
        float elapsedTeleportationTime = 0f;
        Vector3 initialPosition = player.position;
        Transform targetPosition = connectedPortal.spawnLocation;

        // while (elapsedTeleportationTime < TeleportTime)
        // {
        //     float time = elapsedTeleportationTime / TeleportTime;
        //     //Lerp the players position to the target position
        //     player.position = Vector3.Lerp(initialPosition, targetPosition.position, time);
        //     //Slerp the players rotation to the target's rotation
        //     player.localRotation = Quaternion.Slerp(player.localRotation, targetPosition.localRotation, time);
        //
        //     elapsedTeleportationTime += Time.deltaTime;
        //     yield return null;
        // }
        
        CharacterController characterController = player.gameObject.GetComponent<CharacterController>();
        characterController.enabled = false;

        // Move player to the spawn location of the connected portal
        player.position = targetPosition.position;
        // Change the rotation of the player to the targetPosition's rotation
        player.rotation = targetPosition.rotation;

        yield return null;
        
        if (player.position == targetPosition.position && player.rotation == targetPosition.rotation)
        {
            characterController.enabled = true;
            Debug.Log("Re enabling character controller");
        }
        
        // Start cooldown on the connected portal
        connectedPortal._isOnCooldown = true;
        _isOnCooldown = true;
        _cooldownCoroutine = StartCoroutine(StartTeleportCooldown());

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
