using System;
using Unity.VisualScripting;
using UnityEngine;
/* Assignment: Portal
/  Programmer: Alden Chappell
/  Class Section: SGD.285.4171
/  Instructor: Locklear
/  Date: 29/29/2024
*/

[RequireComponent(typeof(Rigidbody))]
public class Pickup : MonoBehaviour
{
    public bool isPickedUp = false;
    [SerializeField] private float floatSpeed = 2.0f;
    [SerializeField] private float floatHeight = 0.5f;

    private void Update()
    {
        if (isPickedUp)
        {
            //Hover();
        }
    }

    private void Hover()
    {
        // Calculate the vertical offset using Mathf.Sin for a smooth floating motion
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        // Apply the offset to the object's position
        transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
    }
}
