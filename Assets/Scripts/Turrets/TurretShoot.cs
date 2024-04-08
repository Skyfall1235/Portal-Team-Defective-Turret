/* Assignment: Portal
/  Programmer: owen Jones
/  Class Section: SGD.285.4171
/  Instructor: Locklear
*/
using System.Collections;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    TurretSphereVision tSV;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform[] barrelPositions;

    GameObject[] pooledBullets;

    bool startedShooting;

    [SerializeField] private float fireDelay = .1f; //Time between each turret shot.
    
    private TurretSound _turretSound;
    private void Start()
    {
        tSV = GetComponentInParent<TurretSphereVision>();
        _turretSound = GetComponentInParent<TurretSound>();
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
            //Added a fire delay to add balance - Alden
            yield return new WaitForSeconds(fireDelay);
           
            //Invoke the turret shooting event
            _turretSound.onShootTurretEvent.Invoke();
            
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
