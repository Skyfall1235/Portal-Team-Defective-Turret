using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSphereVision : MonoBehaviour
{
    public float visionDistance;
    [Range(0, 360)]
    public float visionAngle;

    public LayerMask shootableLayer;

    private List<Transform> targetsInSight = new List<Transform>();

    //Get and set targetsInSight list
    public List<Transform> SpottedTargets
    {
        get
        {
            return targetsInSight;
        }
        set
        {
            targetsInSight = value;
        }

    }
   

    private void Start()
    {
        InvokeRepeating("GetVisibleTargets", .2f, .2f);
    }

    private void GetVisibleTargets()
    {
        SpottedTargets.Clear();
        Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, visionDistance, shootableLayer);
 
        for(int i = 0; i < targetsInRadius.Length; i++)
        {
            if(Vector3.Angle(transform.forward, targetsInRadius[i].transform.position) < visionAngle / 2 || Vector3.Angle(transform.up, targetsInRadius[i].transform.position) < visionAngle / 2)
            {
                print("Target In Angle");
                float distanceToTarget = Vector3.Distance(transform.position, targetsInRadius[i].transform.position);
                Vector3 directionToTarget = (targetsInRadius[i].transform.position - transform.position).normalized;

                if(Physics.Raycast(transform.position, directionToTarget, out RaycastHit hit, distanceToTarget))
                {
                    int shootablesLayerValue = LayerMask.NameToLayer("Player");
                    if(hit.transform.gameObject.layer == shootablesLayerValue)
                    {
                        SpottedTargets.Add(targetsInRadius[i].transform);
                       
                    }
                    
                }
                
            }
        }
    }

    //Used for custom editor script for editor visualization of Viewing distance and Angle.
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, visionDistance);
    }
}
