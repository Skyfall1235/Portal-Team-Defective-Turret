using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    TurretSphereVision tSV;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform[] barrelPositions;

    bool startedShooting;

    private void Start()
    {
        tSV = GetComponentInParent<TurretSphereVision>();
        InvokeRepeating("CheckForTargets", .2f, .2f);
    }

    private void CheckForTargets()
    {
        if (tSV.SpottedTargets.Count > 0)
        {
            if(!startedShooting)
            {
                StartCoroutine(CreateProjectiles());
                startedShooting = true;
            }
            
            
        }
        else
        {
            StopAllCoroutines();
            startedShooting = false;
        }
    }

    private IEnumerator CreateProjectiles()
    {
        int barrelIndex = 0;
        while(true)
        {
            yield return new WaitForSeconds(.1f);
            CreateBullet(barrelIndex);

            barrelIndex += 1;
            if(barrelIndex > 3)
            {
                barrelIndex = 0;
            }
        }

    }

    private void CreateBullet(int barrel)
    {
        GameObject bullet = Instantiate(bulletPrefab, barrelPositions[barrel].position, transform.rotation);
    }
}
