using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float spread;
  
    public void SetBulletShootDir()
    {
        Vector3 shootDir = transform.forward + new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), Random.Range(-spread, spread));
        GetComponent<Rigidbody>().AddForce(shootDir * speed);

        Invoke("ReturnBulletToPool", 2f);
    }

    void ReturnBulletToPool()
    {
        transform.position = transform.GetComponentInParent<Transform>().position;
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerStats>().CancelInvoke();
            other.GetComponent<PlayerStats>().DecreaseHealth();
           

        }
        ReturnBulletToPool();
    }



}
