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
    
    [SerializeField] private float minDistanceToHoldPickup;
    [SerializeField] private float maxDistanceToHoldPickup;
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
        if (Physics.Raycast(
                playerCam.transform.position,
                playerCam.transform.forward,
                out var hitInfo,
                maxDistanceToDetectObjects,
                pickupMask))
        {
            GameObject hitObject = hitInfo.collider.gameObject;

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
    
        // Disable rigidbody gravity temporarily to prevent shaking while picked up.
        Rigidbody currentPickupRigidbody = pickup.GetComponent<Rigidbody>();
        currentPickupRigidbody.useGravity = false;

        Vector3 targetPosition = playerCam.transform.position + playerCam.transform.forward * maxDistanceToHoldPickup;
        _currentPickup.transform.position = Vector3.Lerp(_currentPickup.transform.position, targetPosition, Time.deltaTime * 10f);
    
        // Call the method to move the pickup with the scroll wheel
        MovePickupWithScrollWheel();
    
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

    //Get the input direction of the scroll wheel (up or down)
    private Vector2 GetScrollWheelInputDirection() => Mouse.current.scroll.ReadValue();

    
    //Check if the player is scrolling or not.
    private bool Scrolling() =>
        Mouse.current.scroll.ReadValue().magnitude > 0 || Mouse.current.scroll.ReadValue().magnitude < 0; 
    
    private void MovePickupWithScrollWheel()
    {
        Vector2 scrollDirection = GetScrollWheelInputDirection();
    
        if (scrollDirection.y > 0)
        {
            // Move the pickup away from the player up to the maxDistanceToHoldPickup
            Vector3 targetMaxPosition = playerCam.transform.position + playerCam.transform.forward * maxDistanceToHoldPickup;
            _currentPickup.transform.position = Vector3.Lerp(_currentPickup.transform.position, targetMaxPosition, Time.deltaTime * 10f);
        }
        else if (scrollDirection.y < 0)
        {
            // Move the pickup towards the player up to the minDistanceToHoldPickup
            Vector3 targetMinPosition = playerCam.transform.position + playerCam.transform.forward * minDistanceToHoldPickup;
            _currentPickup.transform.position = Vector3.Lerp(_currentPickup.transform.position, targetMinPosition, Time.deltaTime * 10f);
        }
    }

    //private Vector3 HitPoint(GameObject pickup) => pickup.transform.position;
}

