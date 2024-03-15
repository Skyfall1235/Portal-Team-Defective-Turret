using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCOV : MonoBehaviour
{
    [SerializeField] float maxXRaycastModifier;
    [SerializeField] float maxYRaycastModifier;
    [SerializeField] float maxZRaycastModifier;

    private Vector3 gizmoDirection;

    private void Start()
    {
        InvokeRepeating("RaycastForTargets", .1f, .1f);
        print(Vector3.forward);
    }


    private void RaycastForTargets()
    {
        RaycastHit hit;

        float raycastXModifier = Random.Range(0, maxXRaycastModifier);
        float raycastYModifier = Random.Range(0, maxYRaycastModifier);
     

        Vector3 direction = new Vector3(transform.forward.x + raycastXModifier, transform.forward.y + raycastYModifier, transform.forward.z);
        gizmoDirection = direction;

        if(Physics.Raycast(transform.position, direction, out hit, 5f))
        {
            print(hit.transform.name);
        }
        print("Raycast");
        Debug.DrawRay(transform.position, direction * 1000, Color.yellow);
    }

 
}
