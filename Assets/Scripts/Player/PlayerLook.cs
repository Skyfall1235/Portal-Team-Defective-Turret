using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Assignment: Portal
/  Programmer: Owen Jones
/  Class Section: SGD.285.4171
/  Instructor: Locklear
/  Date: 04/07/2024
*/
public class PlayerLook : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] Transform playerBody;
    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        playerBody.Rotate(Vector3.up * mouseX);
        
    }
}
