using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    bool rotateLeft;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        rotateLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(rotateLeft)
        {
            transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y + speed, transform.position.z));
            if(transform.rotation.y <= 0)
            {
                rotateLeft = false;
            }
        }
        else
        {

        }
    }
}
