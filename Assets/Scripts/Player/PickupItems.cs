using UnityEngine;
using UnityEngine.InputSystem;
/* Assignment: Portal
/  Programmer: Alden Chappell
/  Class Section: SGD.285.4171
/  Instructor: Locklear
/  Date: 02/29/2024
*/
public class PickupItems : MonoBehaviour
{
    private GameObject _currentPickupObject; //Current picked up object, will clear once right click is released.
    
    //Reference to current pickup script in order to respawn after teleporting.
    [HideInInspector] public Pickup currentPickup; 
    
    [SerializeField] private GameObject playerCam;
    
    private const float MinDistanceToHoldPickup = 2.0f; //Closest distance pickups will be held at.
    private const float MaxDistanceToHoldPickup = 8.0f; //Furthest distance pickups will be held at.
    private const float MaxDistanceToDetectObjects = 17.5f; //Furthest distance the player can pick up an object from.
    [SerializeField] private LayerMask pickupMask; //Items should be on the pickup layer mask for the raycast to function.


    private void Update()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            if (_currentPickupObject == null)
            {
                DetectPickup();
            }
            else
            {
                HoldPickup();
            }
        }
        else if (_currentPickupObject != null)
        {
            // Release the pickup if the right mouse button is not pressed or the player left clicks.
            DropPickup();
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
                _currentPickupObject = hitObject;
                currentPickup = hitObject.GetComponent<Pickup>();
                HoldPickup();
            }
            else
            {
                // Reset _currentPickup if the player is not looking at a valid pickup
                _currentPickupObject = null;
                currentPickup = null;
            }
        }
        else
        {
            // Reset _currentPickup if the player is not looking at anything
            _currentPickupObject = null;
            currentPickup = null;
        }
    }
    
    //ON TELEPORT, OBJECT SHOULD STAY WITH PLAYER. MAKE A METHOD TO RESET THE PICKUPS POSITION TO THE PLAYERS WITH A 
    //SMALL OFFSET
    
    private void HoldPickup()
    {
        if (_currentPickupObject == null) return;
    
        //Assign the current pickup as picked up.
        currentPickup = _currentPickupObject.GetComponent<Pickup>();
        currentPickup.isPickedUp = true;
    
        // Disable rigidbody gravity temporarily to prevent shaking while picked up.
        Rigidbody currentPickupRigidbody = _currentPickupObject.GetComponent<Rigidbody>();
        currentPickupRigidbody.useGravity = false;
    
        // Calculate the target position
        Vector3 targetPosition = playerCam.transform.position + playerCam.transform.forward * MaxDistanceToHoldPickup;
        
        // Check if there's an obstacle between the player and the target position
        if (Physics.Raycast(
                playerCam.transform.position,
                playerCam.transform.forward,
                out RaycastHit hitInfo,
                MaxDistanceToHoldPickup * .75f)) // 3/4 of the max distance to hold so the object doesnt 
        {                                                   // move towards the player too early.
            // If there is, set the target position to the hit point of the raycast
            targetPosition = hitInfo.point;
        }
    
        // Calculate the distance from the player to the target position
        float distanceToPlayer = Vector3.Distance(playerCam.transform.position, targetPosition);
    
        // Clamp the distance to be no less than MinDistanceToHoldPickup
        if (distanceToPlayer < MinDistanceToHoldPickup)
        {
            targetPosition = playerCam.transform.position + playerCam.transform.forward * MinDistanceToHoldPickup;
        }
    
        // Move the pickup to the target position
        _currentPickupObject.transform.position = 
            Vector3.Lerp(
                _currentPickupObject.transform.position,
                targetPosition,
                Time.deltaTime * 10f);
    
        if (Mouse.current.leftButton.wasPressedThisFrame && _currentPickupObject != null)
        {
            DropPickup();
        }
    }
    

    /// <summary>
    /// Calling this method will drop the current pickup to the ground wherever it is dropped from.
    /// </summary>
    public void DropPickup()
    {
        if(_currentPickupObject != null)
        {
            // Check if the distance from the player to the pickup is greater than or equal to 10.0f
            float distanceFromPlayer = Vector3.Distance(transform.position, playerCam.transform.position);
            if (distanceFromPlayer >= 10.0f)
            {
                // Move the pickup above the player's head
                _currentPickupObject.GetComponent<Pickup>().RespawnPickupInFrontOfPlayer(gameObject);
            }
        
            // Assign the current pickup as not picked up
            Pickup pickup = _currentPickupObject.GetComponent<Pickup>();
            pickup.isPickedUp = false;
        
            // Empty out the current pickup variable to allow for picking up new pickups
            _currentPickupObject = null;
            currentPickup = null;

            // Reenable rigidbody gravity 
            Rigidbody currentPickupRigidbody = pickup.GetComponent<Rigidbody>();
            currentPickupRigidbody.useGravity = true;
        }
    }
}

