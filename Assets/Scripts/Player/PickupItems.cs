/* Assignment: Portal
/  Programmer: Alden Chappell
/  Class Section: SGD.285.4171
/  Instructor: Locklear
/  Date: 1/29/2024
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class PickupItems : MonoBehaviour
{
    private GameObject _currentPickup; //Current picked up object, will clear once right click is released.

    [SerializeField] private GameObject playerCam;
    
    private const float MinDistanceToHoldPickup = 2.0f; //Closest distance pickups will be held at.
    private const float MaxDistanceToHoldPickup = 8.0f; //Furthest distance pickups will be held at.
    private const float MaxDistanceToDetectObjects = 17.5f; //Furthest distance the player can pick up an object from.
    [SerializeField] private LayerMask pickupMask; //Items should be on the pickup layer mask for the raycast to function.
    
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
        if (Physics.Raycast(
                playerCam.transform.position,
                playerCam.transform.forward,
                out var hitInfo,
                MaxDistanceToDetectObjects,
                pickupMask))
        {
            GameObject hitObject = hitInfo.collider.gameObject;

            // Check if the object has the Pickup component and is not already picked up
            Pickup pickupComponent = hitObject.GetComponent<Pickup>();
            if (pickupComponent != null && !pickupComponent.isPickedUp)
            {
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

    // private void HoldPickup()
    // {
    //     if (_currentPickup == null) return;
    //
    //     StartCoroutine(HoldPickupInPlaceCooldown(HoldCooldownTime));
    //     
    //     Pickup pickup = _currentPickup.GetComponent<Pickup>();
    //     pickup.isPickedUp = true;
    //
    //     // Disable rigidbody gravity temporarily to prevent shaking while picked up.
    //     Rigidbody currentPickupRigidbody = pickup.GetComponent<Rigidbody>();
    //     currentPickupRigidbody.useGravity = false;
    //
    //     Vector3 targetPosition = playerCam.transform.position + playerCam.transform.forward * maxDistanceToHoldPickup;
    //
    //     // Move the pickup only if the player is not scrolling and isn't on hold pickup cooldown.
    //     if (!_isScrolling && _shouldHoldPickup)
    //     {
    //         _currentPickup.transform.position = Vector3.Lerp(_currentPickup.transform.position, targetPosition, Time.deltaTime * 10f);
    //     }
    //
    //     // Call the method to move the pickup with the scroll wheel
    //     MovePickupWithScrollWheel();
    // }
    
    
    private void HoldPickup()
    {
        if (_currentPickup == null) return;

        //Assign the current pickup as picked up.
        Pickup pickup = _currentPickup.GetComponent<Pickup>();
        pickup.isPickedUp = true;

        // Disable rigidbody gravity temporarily to prevent shaking while picked up.
        Rigidbody currentPickupRigidbody = pickup.GetComponent<Rigidbody>();
        currentPickupRigidbody.useGravity = false;

        // Calculate the target position
        Vector3 targetPosition = playerCam.transform.position + playerCam.transform.forward * MaxDistanceToHoldPickup;

        // Check if there's an obstacle between the player and the target position
        if (Physics.Raycast(
                playerCam.transform.position,
                playerCam.transform.forward,
                out RaycastHit hit,
                MaxDistanceToHoldPickup * .75f)) // 3/4 of the max distance to hold so the object doesnt 
                                                            // move towards the player too early.
        {
            // If there is, set the target position to the hit point of the raycast
            targetPosition = hit.point;
        }

        // Calculate the distance from the player to the target position
        float distanceToPlayer = Vector3.Distance(playerCam.transform.position, targetPosition);

        // Clamp the distance to be no less than MinDistanceToHoldPickup
        if (distanceToPlayer < MinDistanceToHoldPickup)
        {
            targetPosition = playerCam.transform.position + playerCam.transform.forward * MinDistanceToHoldPickup;
        }

        // Move the pickup to the target position
        _currentPickup.transform.position = 
            Vector3.Lerp(
                _currentPickup.transform.position,
                targetPosition,
                Time.deltaTime * 10f);

        if (Mouse.current.leftButton.wasPressedThisFrame && _currentPickup != null)
        {
            DropItem();
        }
    }

    
    private void DropItem()
    {
        //Assign the current pickup as not picked up.
        Pickup pickup = _currentPickup.GetComponent<Pickup>();
        pickup.isPickedUp = false;
        
        //Empty out the current pickup variable to allow for picking up new pickups.
        _currentPickup = null;

        //Reenable rigidbody gravity 
        Rigidbody currentPickupRigidbody = pickup.GetComponent<Rigidbody>();
        currentPickupRigidbody.useGravity = true;
    }
}

