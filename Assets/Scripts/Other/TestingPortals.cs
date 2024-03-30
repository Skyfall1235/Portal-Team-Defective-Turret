using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingPortals : MonoBehaviour
{
    public Transform spawnLocation; // The spawn location for the player after teleportation
    public TestingPortals connectedPortal; // The portal that this portal is connected to
    private const float TeleportTime = 1.0f; // Time taken for teleportation
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
        Vector3 targetPosition = connectedPortal.spawnLocation.position;

        while (elapsedTime < TeleportTime)
        {
            float t = elapsedTime / TeleportTime;
            player.position = Vector3.Lerp(initialPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Move player to the spawn location of the connected portal
        player.position = targetPosition;

        // Start cooldown
        _isOnCooldown = true;
        _cooldownCoroutine = StartCoroutine(StartTeleportCooldown());

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
