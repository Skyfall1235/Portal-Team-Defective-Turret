using System.Collections;
using UnityEngine;

public class TestingPortals : MonoBehaviour
{
    public Transform spawnLocation; // The spawn location for the player after teleportation
    public TestingPortals connectedPortal; // The portal that this portal is connected to
    private const float TeleportTime = .075f; // Time taken for teleportation
    private const float TeleportCooldown = 1.5f; // Cooldown time between teleportations
    private bool _isTeleporting; // Flag to prevent multiple teleportations
    private bool _isOnCooldown; // Flag to prevent teleportation during cooldown
    private Coroutine _cooldownCoroutine; // Coroutine for cooldown

    private void OnTriggerEnter(Collider other)
    {
        if (!_isTeleporting && !_isOnCooldown && other.CompareTag("Player"))
        {
            StartCoroutine(TeleportPlayer(other.transform));
        }
    }

    private IEnumerator TeleportPlayer(Transform player)
    {
        _isTeleporting = true;

        
        // Start teleportation effect
        float elapsedTime = 0f;
        Vector3 initialPosition = player.position;
        Transform targetPosition = connectedPortal.spawnLocation;

        while (elapsedTime < TeleportTime)
        {
            float t = elapsedTime / TeleportTime;
            //Lerp the players position to the target position
            player.position = Vector3.Lerp(initialPosition, targetPosition.position, t);
            //Lerp the players rotation to the target's rotation
            player.localRotation = Quaternion.Slerp(player.localRotation, targetPosition.localRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Move player to the spawn location of the connected portal
        player.position = targetPosition.position;
        // Change the rotation of the player to the targetPosition's rotation
        player.rotation = targetPosition.rotation;

        // Start cooldown
        _isOnCooldown = true;
        _cooldownCoroutine = StartCoroutine(StartTeleportCooldown());

        // Reset teleporting flag
        _isTeleporting = false;
    }

    private IEnumerator StartTeleportCooldown()
    {
        yield return new WaitForSeconds(TeleportCooldown);

        _isOnCooldown = false;
    }


    private void OnDrawGizmosSelected()
    {
        if (connectedPortal != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, connectedPortal.transform.position);
        }
    }
}
