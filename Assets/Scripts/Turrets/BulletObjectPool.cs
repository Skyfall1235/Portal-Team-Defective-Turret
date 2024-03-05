using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletObjectPool : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    int maxPoolSize = 100;
    bool collectionChecks = true;

    IObjectPool<GameObject> objectPool;

    public IObjectPool<GameObject> ObjectPool
    {
        get
        {
            if (objectPool == null)
            {
                objectPool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize);

            }
            return objectPool;
        }
    }
        

    GameObject CreatePooledItem()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        ReturnToPool returnToPool = bulletInstance.AddComponent<ReturnToPool>();
        returnToPool.pool = objectPool;
        return bulletInstance;
    }

    void OnTakeFromPool(GameObject bullet)
    {
        bullet.SetActive(true);
    }

    void OnReturnToPool(GameObject bullet)
    {
        bullet.SetActive(false);
    }

    void OnDestroyPoolObject(GameObject bullet)
    {
        Destroy(bullet);
    }



    /*
     * Put these at the top of the script that needs to get bullets:
     * GameObject current"Object"
     * IObjectPool<GameObjcet> "object"Pool
     * 
     * Initialize objectPool:
     * objectPool = FindObjectOfType<PoolManager>().GetComponent<PoolManager>().ObjectPool;
     * 
     * Put this where you need to grab the bullet:
     * current"Object" = "object"Pool.Get();
     */


}
