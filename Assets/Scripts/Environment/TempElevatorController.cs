using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/* Assignment: Portal
/  Programmer: Alden Chappell
/  Class Section: SGD.285.4171
/  Instructor: Locklear
/  Date: 03/31/2024
*/

public class TempElevatorController : MonoBehaviour
{
    [Header("This script should be attached to the trigger object which \n should be a child of the elevator platform.")]
    [Space(10)]
    
    [SerializeField] private Transform topPosition; //Where the elevator should reach
    private Vector3 _startingPosition; //Where the elevator starts

    private GameObject _elevatorPlatform;
    
    
    //How long it takes for the elevator to reach its next destination
    [SerializeField] private float totalElevatorMovementTime = 5.0f;

    private bool _isElevatorAtTop;
    private bool _canUseElevator = true;
    private const float ElevatorUsageCooldown = 3.0f;

    private void Start()
    {
        _elevatorPlatform = transform.parent.gameObject;
        _startingPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !_canUseElevator) return;

        if (!_isElevatorAtTop)
        {
            MoveElevatorToTop();
        }
        else
        {
            ReturnElevatorToStartingPosition();
        }
    }

    private void MoveElevatorToTop()
    {
        StartCoroutine(ElevateCoroutine());
    }

    private IEnumerator ElevateCoroutine()
    {
        float elapsedElevationTime = 0.0f;
        Vector3 initialPosition = _elevatorPlatform.transform.position;

        while (elapsedElevationTime < totalElevatorMovementTime)
        {
            float time = elapsedElevationTime / totalElevatorMovementTime;

            _elevatorPlatform.transform.position = Vector3.Lerp(
                initialPosition,
                topPosition.position,
                time);

            elapsedElevationTime += Time.deltaTime;
            yield return null;
        }

        // Ensure elevator reaches exactly the top position
        _elevatorPlatform.transform.position = topPosition.position;
        _isElevatorAtTop = true;
        
        // Start the elevator cooldown.
        StartCoroutine(ElevatorCooldown(ElevatorUsageCooldown));
    }

    private void ReturnElevatorToStartingPosition()
    {
        StartCoroutine(ReturnCoroutine());
    }

    private IEnumerator ReturnCoroutine()
    {
        float elapsedElevationTime = 0.0f;
        Vector3 initialPosition = _elevatorPlatform.transform.position;

        while (elapsedElevationTime < totalElevatorMovementTime)
        {
            float time = elapsedElevationTime / totalElevatorMovementTime;
            _elevatorPlatform.transform.position = Vector3.Lerp(
                initialPosition,
                _startingPosition,
                time);
            elapsedElevationTime += Time.deltaTime;
            yield return null;
        }

        // Ensure elevator reaches exactly the starting position
        _elevatorPlatform.transform.position = _startingPosition;
        _isElevatorAtTop = false;

        // Start the elevator cooldown.
        StartCoroutine(ElevatorCooldown(ElevatorUsageCooldown));
    }

    //A cooldown to prevent elevator from going up and down as soon as player steps off.
    private IEnumerator ElevatorCooldown(float timeBetweenElevatorUsages)
    {
        _canUseElevator = false;
        yield return new WaitForSeconds(timeBetweenElevatorUsages);
        _canUseElevator = true;
    }
}
