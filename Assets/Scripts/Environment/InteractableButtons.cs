/* Assignment: Portal
/  Programmer: FILL NAME HERE
/  Class Section: SGD.285.4171
/  Instructor: Locklear
*/
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InteractableButtons : MonoBehaviour
{
    public UnityEvent<bool> OnCollision = new UnityEvent<bool>();

    [SerializeField] private GameObject explosion;

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.layer == 6)
        {
            OnCollision.Invoke(false);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
 
        if (collision.gameObject.layer == 6)
        {
            OnCollision.Invoke(true);
        }
    }

    public void OnInteractionDestroyTurret(GameObject turret)
    {
        if (turret != null)
        {
            GameObject turretExplosion = Instantiate(explosion, turret.transform.position, Quaternion.identity);
        
            StartCoroutine(TurretExplosion(turretExplosion, turret));
        }
    }

    private IEnumerator TurretExplosion(GameObject explosionObj,GameObject turret)
    {
        Destroy(turret);
        yield return new WaitForSeconds(2.0f);
        Destroy(explosionObj);
    }

}
