using System;
using UnityEngine;
using UnityEngine.InputSystem;

/* Assignment: Portal
/  Programmer: Alden Chappell
/  Class Section: SGD.285.4171
/  Instructor: Locklear
/  Date: 29/29/2024
*/
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupItems : MonoBehaviour
{
    private GameObject _currentPickup;
    private bool _isDetectingAPickup = false;

    [SerializeField] private float maxDistanceToHold;
    [SerializeField] private float maxDistanceToDetectObjects;
    [SerializeField] private LayerMask pickupMask;

    private void Update()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            if (_currentPickup == null)
            {
                DetectPickup();
            }
            else
            {
                HoldPickup();
            }
        }
        else if (_currentPickup != null)
        {
            // Release the pickup if the right mouse button is not pressed
            DropItem();
        }
    }

    private void DetectPickup()
    {
        RaycastHit[] hits = new RaycastHit[1];

        if (Physics.RaycastNonAlloc(
                transform.position,
                transform.forward,
                hits,
                maxDistanceToDetectObjects,
                pickupMask) > 0)
        {
            GameObject hitObject = hits[0].collider.gameObject;

            // Check if the object has the Pickup component and is not already picked up
            Pickup pickupComponent = hitObject.GetComponent<Pickup>();
            if (pickupComponent != null && !pickupComponent.isPickedUp)
            {
                _currentPickup = hitObject;
            }
            else
            {
                // Reset _currentPickup if the player is not looking at a valid pickup
                _currentPickup = null;
            }
        }
        else
        {
            // Reset _currentPickup if the player is not looking at anything
            _currentPickup = null;
        }
    }

    private void HoldPickup()
    {
        Pickup pickup = _currentPickup.GetComponent<Pickup>();
        pickup.isPickedUp = true;

        Vector3 targetPosition = HitPoint(_currentPickup);
        _currentPickup.transform.position = Vector3.Lerp(_currentPickup.transform.position, targetPosition, Time.deltaTime * 10f);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            DropItem();
        }
    }

    private void DropItem()
    {
        Pickup pickup = _currentPickup.GetComponent<Pickup>();
        pickup.isPickedUp = false;
        _currentPickup = null;
    }

    private Vector3 HitPoint(GameObject pickup) => pickup.transform.position;
}

