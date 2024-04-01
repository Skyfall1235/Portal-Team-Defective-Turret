using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    TurretSphereVision tSV;
    Quaternion originalRotation;

    [SerializeField] Transform[] barrels;

    private void Start()
    {
        tSV = GetComponentInParent<TurretSphereVision>();
        originalRotation = transform.rotation;
        print(barrels.Length);
    }

    private void Update()
    {
        if(tSV.SpottedTargets.Count > 0)
        {
            Vector3 capDirection = new Vector3(tSV.SpottedTargets[0].position.x - transform.position.x, 0, tSV.SpottedTargets[0].position.z - transform.position.z).normalized;
            Quaternion capRotation = Quaternion.LookRotation(capDirection, Vector3.up);
            transform.rotation = capRotation;

           
            for (int i = 0; i < barrels.Length; i++)
            {
                print("rotating");
                Vector3 barrelDirection = (tSV.SpottedTargets[0].position - barrels[i].position).normalized;
                Quaternion barrelRotation = Quaternion.LookRotation(barrelDirection, Vector3.up);
                barrels[i].rotation = barrelRotation;
            }

        }
        else
        {
            transform.rotation = originalRotation;
        }
    }
}
