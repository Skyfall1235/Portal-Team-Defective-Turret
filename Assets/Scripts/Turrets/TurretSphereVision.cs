using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSphereVision : MonoBehaviour
{
    public float visionDistance;
    [Range(0, 360)]
    public float visionAngle;

    public LayerMask shootableLayer;

    public List<Transform> targetsInSight = new List<Transform>();


    private void Start()
    {
        StartCoroutine(CheckForTargets(.2f));
    }


    private IEnumerator CheckForTargets(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            GetVisibleTargets(visionDistance, visionAngle, shootableLayer);
        }
    }

    private void GetVisibleTargets(float radius, float angle, LayerMask shootables)
    {
        targetsInSight.Clear();
        Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, radius, shootables);
        print(targetsInRadius.Length);
        for(int i = 0; i < targetsInRadius.Length; i++)
        {
            if(Vector3.Angle(transform.forward, targetsInRadius[i].transform.position) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, targetsInRadius[i].transform.position);
                Vector3 directionToTarget = (targetsInRadius[i].transform.position - transform.position).normalized;

                if(Physics.Raycast(transform.position, directionToTarget, out RaycastHit hit, distanceToTarget))
                {
                    int shootablesLayerValue = LayerMask.NameToLayer("Player");
                    if(hit.transform.gameObject.layer == shootablesLayerValue)
                    {
                        targetsInSight.Add(targetsInRadius[i].transform);
                    }
                }
                
            }
        }
    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
