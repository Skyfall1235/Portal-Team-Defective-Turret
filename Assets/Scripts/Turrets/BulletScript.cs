using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float spread;
  

    private void Start()
    {
        Destroy(this.gameObject, 6f);
        Vector3 shootDir = transform.forward + new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), Random.Range(-spread, spread));
        GetComponent<Rigidbody>().AddForce(shootDir * speed);
    }

    private void OnTriggerEnter(Collider other)
    {

        Destroy(gameObject);
    }



}
