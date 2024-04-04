using UnityEngine;
using UnityEngine.Events;
/* Assignment: Portal
/  Programmer: Alden Chappell
/  Class Section: SGD.285.4171
/  Instructor: Locklear
/  Date: 04/04/2024
*/
public class TurretSound : MonoBehaviour
{
    private AudioSource _turretAudio;

    public UnityEvent onShootTurretEvent;
    private void Awake()
    {
        _turretAudio = GetComponent<AudioSource>();
    }

    public void PlayTurretSound(AudioClip sound)
    {
        _turretAudio.PlayOneShot(sound);
    }
}
