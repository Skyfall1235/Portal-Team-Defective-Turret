using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletList : MonoBehaviour
{
    [SerializeField] GameObject[] bullets;

    public GameObject[] GetPooledBullets
    {
        get
        {
           return bullets;
        }
    }
}
