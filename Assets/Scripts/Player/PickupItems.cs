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
    private GameObject _currentPickup; //Current picked up object, will clear once right click is released.

    [SerializeField] private GameObject playerCam;
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
            // Release the pickup if the right mouse button is not pressed or the player left clicks.
            DropItem();
        }
    }

    private void DetectPickup()
    {
        RaycastHit[] hits = new RaycastHit[1];

        if (Physics.RaycastNonAlloc(
                playerCam.transform.position,
                playerCam.transform.forward,
                hits,
                maxDistanceToDetectObjects,
                pickupMask) > 0)
        {
            GameObject hitObject = hits[0].collider.gameObject;

            // Check if the object has the Pickup component and is not already picked up
            Pickup pickupComponent = hitObject.GetComponent<Pickup>();
            if (pickupComponent != null && !pickupComponent.isPickedUp)
            {
                Debug.Log("Hit pickup-able item.");
                _currentPickup = hitObject;
                HoldPickup();
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
        if (_currentPickup == null) return;

        Pickup pickup = _currentPickup.GetComponent<Pickup>();
        pickup.isPickedUp = true;
        
        //Disable rigidbody gravity temporarily to prevent shaking while picked up.
        Rigidbody currentPickupRigidbody = pickup.GetComponent<Rigidbody>();
        currentPickupRigidbody.useGravity = false;

        Vector3 targetPosition = playerCam.transform.position + playerCam.transform.forward * maxDistanceToHold;
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

        //Reenable rigidbody gravity 
        Rigidbody currentPickupRigidbody = pickup.GetComponent<Rigidbody>();
        currentPickupRigidbody.useGravity = true;
    }

    //private Vector3 HitPoint(GameObject pickup) => pickup.transform.position;
}

