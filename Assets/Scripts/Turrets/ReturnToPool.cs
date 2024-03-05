using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ReturnToPool : MonoBehaviour
{
    public IObjectPool<GameObject> pool;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Return", 5);
    }

  void Return()
    {
        pool.Release(gameObject);
    }
}
