using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    TurretSphereVision tSV;
    Quaternion originalRotation;

    private void Start()
    {
        tSV = GetComponentInParent<TurretSphereVision>();
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        if(tSV.SpottedTargets.Count > 0)
        {
            Vector3 direction = (tSV.SpottedTargets[0].position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = rotation;
        }
        else
        {
            transform.rotation = originalRotation;
        }
    }
}
