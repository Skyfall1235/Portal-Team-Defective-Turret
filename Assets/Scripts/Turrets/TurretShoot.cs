using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    TurretSphereVision tSV;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform[] barrelPositions;

    GameObject[] pooledBullets;

    bool startedShooting;

    private void Start()
    {
        tSV = GetComponentInParent<TurretSphereVision>();
        pooledBullets = GameObject.Find("BulletPool").GetComponent<BulletList>().GetPooledBullets;

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
           
            for(int i = 0; i < pooledBullets.Length; i++)
            {
                if(pooledBullets[i].activeSelf == false)
                {
                    CreateBullet(barrelPositions[barrelIndex], pooledBullets[i]);
                    break;
                }
            }

            barrelIndex += 1;
            if(barrelIndex > 3)
            {
                barrelIndex = 0;
            }
        }
    }

    private void CreateBullet(Transform barrel, GameObject bullet)
    {

        bullet.gameObject.SetActive(true);
        bullet.transform.position = barrel.position;
        bullet.transform.rotation = barrel.rotation;
        bullet.GetComponent<BulletScript>().SetBulletShootDir();

    }
}
